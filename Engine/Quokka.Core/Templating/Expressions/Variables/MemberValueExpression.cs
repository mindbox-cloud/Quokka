using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
    internal class MemberValueExpression : VariantValueExpression
    {
	    private readonly VariableValueExpression ownerExpression;

	    private readonly IReadOnlyList<Member> members;

	    public override TypeDefinition GetResultType(SemanticAnalysisContext context)
	    {
		    return TypeDefinition.Unknown;
	    }

		public MemberValueExpression(VariableValueExpression ownerExpression, IReadOnlyList<Member> members)
	    {
		    this.ownerExpression = ownerExpression;
		    this.members = members;
	    }

	    public override void CompileVariableDefinitions(SemanticAnalysisContext context, TypeDefinition expectedExpressionType)
	    {
		    ownerExpression.CompileVariableDefinitions(context, TypeDefinition.Composite);

		    var ownerVariableDefinition = ownerExpression.GetVariableDefinition(context);

			for (int i = 0; i < members.Count; i++)
		    {
			    var memberType = i == members.Count - 1
									? expectedExpressionType
									: TypeDefinition.Composite;
			    
			    members[i].CompileMemberVariableDefinition(ownerVariableDefinition, memberType);
			    ownerVariableDefinition = members[i].GetMemberVariableDefinition(ownerVariableDefinition);
		    }
	    }

	    public override void RegisterIterationOverExpressionResult(SemanticAnalysisContext context, VariableDefinition iterationVariable)
	    {
		    var leafMemberVariableDefinition = GetLeafMemberVariableDefinition(context);
		    leafMemberVariableDefinition.AddCollectionElementVariable(iterationVariable);
	    }

	    public VariableDefinition GetLeafMemberVariableDefinition(SemanticAnalysisContext context)
	    {
		    var leafMemberVariableDefinition = ownerExpression.GetVariableDefinition(context);

		    foreach (var member in members)
			    leafMemberVariableDefinition = member.GetMemberVariableDefinition(leafMemberVariableDefinition);

		    return leafMemberVariableDefinition;
	    }

	    public override IModelDefinition GetExpressionResultModelDefinition(SemanticAnalysisContext context)
	    {
		    return new PrimitiveModelDefinition(TypeDefinition.Unknown);
	    }

	    public override VariableValueStorage Evaluate(RenderContext renderContext)
	    {
		    var value = ownerExpression.Evaluate(renderContext);

		    for (var i = 0; i < members.Count; i++)
		    {
			    value = members[i].GetMemberValue(value);

			    if (value == null || value.CheckIfValueIsNull())
			    {
				    var memberChainStringRepresentation =
					    string.Join(
						    ".",
						    new[] { ownerExpression.StringRepresentation }
							    .Concat(
								    members
									    .Select(m => m.StringRepresentation)
									    .Take(i + 1)));

				    throw new UnrenderableTemplateModelException(
					    $"An attempt to use the value of \"{memberChainStringRepresentation}\" expression which happens to be null");
			    }
		    }

		    return value;
	    }

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			if (ownerExpression.CheckIfExpressionIsNull(renderContext))
				return true;

			var value = ownerExpression.TryGetValueStorage(renderContext);

			foreach (Member member in members)
			{
				value = member.GetMemberValue(value);
				if (value == null || value.CheckIfValueIsNull())
					return true;
			}

			return false;
		}
    }
}
