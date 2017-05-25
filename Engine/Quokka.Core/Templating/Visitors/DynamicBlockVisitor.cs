using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
    internal class DynamicBlockVisitor : QuokkaBaseVisitor<ITemplateNode>
    {
	    public DynamicBlockVisitor(VisitingContext visitingContext) 
			: base(visitingContext)
	    {
	    }

	    public override ITemplateNode VisitIfStatement(QuokkaParser.IfStatementContext context)
	    {
		    var conditionsVisitor = new ConditionsVisitor(VisitingContext);
		    var conditions = new List<ConditionBlock>
		    {
			    context.ifCondition().Accept(conditionsVisitor)
		    };
		    conditions.AddRange(context.elseIfCondition()
			    .Select(elseIf => elseIf.Accept(conditionsVisitor)));
		    if (context.elseCondition() != null)
			    conditions.Add(context.elseCondition().Accept(conditionsVisitor));

		    return new IfBlock(conditions);
	    }

	    public override ITemplateNode VisitForStatement(QuokkaParser.ForStatementContext context)
	    {
		    var forInstruction = context.forInstruction();

		    var iterationVariableIdentifier = forInstruction.iterationVariable().Identifier();

		    return new ForBlock(
			    context.templateBlock()?.Accept(new TemplateVisitor(VisitingContext)),
			    new VariableDeclaration(
				    iterationVariableIdentifier.GetText(),
				    GetLocationFromToken(iterationVariableIdentifier.Symbol),
				    TypeDefinition.Unknown),
			    forInstruction.variantValueExpression().Accept(new VariantValueExpressionVisitor(VisitingContext)));
	    }

	    public override ITemplateNode VisitCommentBlock(QuokkaParser.CommentBlockContext context)
	    {
		    return null;
	    }
	}
}
