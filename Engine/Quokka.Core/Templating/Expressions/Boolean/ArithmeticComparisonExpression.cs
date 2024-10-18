// // Copyright 2022 Mindbox Ltd
// //
// // Licensed under the Apache License, Version 2.0 (the "License");
// // you may not use this file except in compliance with the License.
// // You may obtain a copy of the License at
// //
// //     http://www.apache.org/licenses/LICENSE-2.0
// //
// // Unless required by applicable law or agreed to in writing, software
// // distributed under the License is distributed on an "AS IS" BASIS,
// // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// // See the License for the specific language governing permissions and
// // limitations under the License.

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

		public override void Accept(ITreeVisitor treeVisitor)
		{
			treeVisitor.VisitArithmeticComparisonExpression(operation.ToString());
			
			left.Accept(treeVisitor);
			right.Accept(treeVisitor);
			
			treeVisitor.EndVisit();
		}
	}
}
