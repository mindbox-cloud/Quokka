using System;

namespace Mindbox.Quokka
{
	internal class StringComparisonExpression : BooleanExpression
	{
		private readonly VariantValueExpression variantValueExpression;
		private readonly StringExpression stringExpression;
		private readonly ComparisonOperation comparisonOperation;

		public StringComparisonExpression(
			VariantValueExpression variantValueExpression,
			StringExpression stringExpression,
			ComparisonOperation comparisonOperation)
		{
			if (comparisonOperation != ComparisonOperation.Equals && comparisonOperation != ComparisonOperation.NotEquals)
				throw new ArgumentOutOfRangeException(nameof(comparisonOperation));

			this.variantValueExpression = variantValueExpression;
			this.stringExpression = stringExpression;
			this.comparisonOperation = comparisonOperation;
		}

		public override bool GetBooleanValue(RenderContext renderContext)
		{
			var variableValue = (string)variantValueExpression.Evaluate(renderContext).GetPrimitiveValue();
			var stringValue = (string)stringExpression.Evaluate(renderContext).GetPrimitiveValue();

			bool areStringsEqual = string.Equals(
				variableValue,
				stringValue,
				StringComparison.OrdinalIgnoreCase);

			switch (comparisonOperation)
			{
				case ComparisonOperation.Equals:
					return areStringsEqual;
				case ComparisonOperation.NotEquals:
					return !areStringsEqual;
				default:
					throw new InvalidOperationException("Unsupported comparison operation");
			}
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			variantValueExpression.PerformSemanticAnalysis(context, TypeDefinition.String);
		}
	}
}
