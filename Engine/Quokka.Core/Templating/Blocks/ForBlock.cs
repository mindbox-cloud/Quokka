using System;
using System.IO;
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

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			var innerSemanticContext = context.CreateNestedScopeContext();
			var iterationVariableDefinition = innerSemanticContext.VariableScope.DeclareVariable(
				iterationVariableName,
				new ValueUsage(iterationVariableLocation, TypeDefinition.Unknown));

			enumerableExpression.PerformSemanticAnalysis(context, TypeDefinition.Array);
			block?.PerformSemanticAnalysis(innerSemanticContext);
			enumerableExpression.RegisterIterationOverExpressionResult(context, iterationVariableDefinition);

			var enumerableElementModelDefinition =
				enumerableExpression.GetExpressionResultModelDefinition(context) is IArrayModelDefinition arrayModelDefinition
					? arrayModelDefinition.ElementModelDefinition
					: new PrimitiveModelDefinition(TypeDefinition.Unknown);

			iterationVariableDefinition.ValidateAgainstExpectedModelDefinition(
				enumerableElementModelDefinition,
				context.ErrorListener);
		}

		public override void Render(TextWriter resultWriter, RenderContext renderContext)
		{
			if (block == null)
				return;

			var collectionValue = enumerableExpression.Evaluate(renderContext).GetElements();

			foreach (var collectionElement in collectionValue)
			{
				var innerScope =
					renderContext.VariableScope.CreateChildScope(
						new CompositeVariableValueStorage(iterationVariableName, collectionElement));

				block.Render(resultWriter, renderContext.CreateInnerContext(innerScope));
			}
		}

		public override void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context)
		{
			block?.CompileGrammarSpecificData(context);
		}
	}
}
