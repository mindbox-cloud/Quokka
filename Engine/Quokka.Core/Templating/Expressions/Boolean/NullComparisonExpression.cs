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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
	internal class NullComparisonExpression : BooleanExpression
	{
		private readonly VariantValueExpression variantValueExpression;
		private readonly ComparisonOperation comparisonOperation;

		public NullComparisonExpression(VariantValueExpression variantValueExpression, ComparisonOperation comparisonOperation)
		{
			if (comparisonOperation != ComparisonOperation.Equals && comparisonOperation != ComparisonOperation.NotEquals)
				throw new ArgumentOutOfRangeException(nameof(comparisonOperation));

			this.variantValueExpression = variantValueExpression;
			this.comparisonOperation = comparisonOperation;
		}

		public override bool GetBooleanValue(RenderContext renderContext)
		{
			bool isValueNull = variantValueExpression.CheckIfExpressionIsNull(renderContext);

			switch (comparisonOperation)
			{
				case ComparisonOperation.Equals:
					return isValueNull;
				case ComparisonOperation.NotEquals:
					return !isValueNull;
				default:
					throw new InvalidOperationException("Unsupported comparison operation");
			}
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			variantValueExpression.PerformSemanticAnalysis(context, TypeDefinition.Unknown);
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			return false;
		}
		public override ExpressionDTO GetTreeDTO()
		{
			var dto = base.GetTreeDTO();
			dto.type = "NullComparisonExpression";
			dto.members = new List<ExpressionDTO> { variantValueExpression.GetTreeDTO() };
			dto.comparisonOperation = comparisonOperation.ToString() ?? "";

			return dto;
		}
	}
}
