using System;

namespace Quokka
{
	internal class StringComparisonExpression : BooleanExpressionBase
	{
		private readonly VariableOccurence variableOccurence;
		private readonly string stringConstant;
		private readonly ComparisonOperation comparisonOperation;

		public StringComparisonExpression(
			VariableOccurence variableOccurence,
			string stringConstant,
			ComparisonOperation comparisonOperation)
		{
			if (comparisonOperation != ComparisonOperation.Equals && comparisonOperation != ComparisonOperation.NotEquals)
				throw new ArgumentOutOfRangeException(nameof(comparisonOperation));

			this.variableOccurence = variableOccurence;
			this.stringConstant = stringConstant;
			this.comparisonOperation = comparisonOperation;
		}

		public override bool Evaluate(RenderContext renderContext)
		{
			var variableValue = (string)renderContext.VariableScope.GetVariableValue(variableOccurence);
			bool areStringsEqual = string.Equals(
				variableValue,
				stringConstant,
				StringComparison.CurrentCultureIgnoreCase);

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

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			context.VariableScope.CreateOrUpdateVariableDefinition(variableOccurence);
		}
	}
}
