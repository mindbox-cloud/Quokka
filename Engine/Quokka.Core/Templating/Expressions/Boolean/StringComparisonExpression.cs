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
			stringExpression.PerformSemanticAnalysis(context, TypeDefinition.String);
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			return variantValueExpression.CheckIfExpressionIsNull(renderContext) 
				|| stringExpression.CheckIfExpressionIsNull(renderContext);
		}
	}
}
