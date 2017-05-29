using System;
using System.Text;

namespace Mindbox.Quokka
{
	internal class ForBlock : TemplateNodeBase
	{
		private readonly ITemplateNode block;
		private readonly string iterationVariableName;
		private readonly Location iterationVariableLocation;
		private readonly VariantValueExpression enumerableExpression;

		public ForBlock(
			ITemplateNode block,
			string iterationVariableName, 
			Location iterationVariableLocation,
			VariantValueExpression enumerableExpression)
		{
			this.block = block;
			this.iterationVariableName = iterationVariableName;
			this.iterationVariableLocation = iterationVariableLocation;
			this.enumerableExpression = enumerableExpression;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			var innerSemanticContext = context.CreateNestedScopeContext();
			var iterationVariableDefinition = innerSemanticContext.VariableScope.DeclareVariable(
				iterationVariableName,
				new ValueUsage(iterationVariableLocation, TypeDefinition.Unknown));

			enumerableExpression.CompileVariableDefinitions(context, TypeDefinition.Array);
			block?.CompileVariableDefinitions(innerSemanticContext);
			enumerableExpression.RegisterIterationOverExpressionResult(context, iterationVariableDefinition);
			
			iterationVariableDefinition.ValidateAgainstExpectedModelDefinition(
				enumerableExpression.GetExpressionResultModelDefinition(context),
				context.ErrorListener);
		}

		public override void Render(StringBuilder resultBuilder, RenderContext renderContext)
		{
			if (block == null)
				return;

			var collectionValue = enumerableExpression.Evaluate(renderContext).GetElements();

			foreach (var collectionElement in collectionValue)
			{
				var innerScope =
					renderContext.VariableScope.CreateChildScope(
						new CompositeVariableValueStorage(iterationVariableName, collectionElement));

				block.Render(resultBuilder, renderContext.CreateInnerContext(innerScope));
			}
		}

		public override void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context)
		{
			block?.CompileGrammarSpecificData(context);
		}
	}
}
