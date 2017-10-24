using System;

namespace Mindbox.Quokka
{
	internal class ArithmeticComparisonExpression : BooleanExpression
	{
		private const double epsilon = 1e-7;

		private readonly ArithmeticExpression left;
		private readonly ArithmeticExpression right;
		private readonly ComparisonOperation operation;

		public ArithmeticComparisonExpression(
			ComparisonOperation operation,
			ArithmeticExpression left,
			ArithmeticExpression right)
		{
			this.operation = operation;
			this.left = left;
			this.right = right;
		}

		public override bool GetBooleanValue(RenderContext renderContext)
		{
			double leftValue = left.GetValue(renderContext);
			double rightValue = right.GetValue(renderContext);

			switch (operation)
			{
				case ComparisonOperation.Equals:
					return Math.Abs(leftValue - rightValue) < epsilon;
				case ComparisonOperation.NotEquals:
					return Math.Abs(leftValue - rightValue) > epsilon;
				case ComparisonOperation.LessThan:
					return (rightValue - leftValue) > epsilon;
				case ComparisonOperation.GreaterThan:
					return (leftValue - rightValue) > epsilon;
				case ComparisonOperation.LessThanOrEquals:
					return ((rightValue - leftValue) > epsilon) 
						|| (Math.Abs(leftValue - rightValue) < epsilon);
                case ComparisonOperation.GreaterThanOrEquals:
					return ((leftValue - rightValue) > epsilon)
						|| (Math.Abs(leftValue - rightValue) < epsilon);
				default:
					throw new NotImplementedException("Unsupported comparison operation");
			}
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			left.PerformSemanticAnalysis(context);
			right.PerformSemanticAnalysis(context);
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			return left.CheckIfExpressionIsNull(renderContext) || right.CheckIfExpressionIsNull(renderContext);
		}
	}
}
