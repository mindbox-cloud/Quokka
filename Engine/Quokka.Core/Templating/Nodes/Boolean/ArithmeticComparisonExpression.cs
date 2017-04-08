using System;

namespace Mindbox.Quokka
{
	internal class ArithmeticComparisonExpression : BooleanExpressionBase
	{
		private const double epsilon = 1e-7;

		private readonly IArithmeticExpression left;
		private readonly IArithmeticExpression right;
		private readonly ComparisonOperation operation;

		public ArithmeticComparisonExpression(
			ComparisonOperation operation,
			IArithmeticExpression left,
			IArithmeticExpression right)
		{
			this.operation = operation;
			this.left = left;
			this.right = right;
		}

		public override bool Evaluate(RenderContext renderContext)
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

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			left.CompileVariableDefinitions(context);
			right.CompileVariableDefinitions(context);
		}
	}
}
