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
	internal class VariantValueArithmeticExpression : ArithmeticExpression
	{
		private readonly VariantValueExpression variantValueExpression;

		public VariantValueArithmeticExpression(VariantValueExpression variantValueExpression)
		{
			this.variantValueExpression = variantValueExpression;
		}

		public override TypeDefinition GetResultType(AnalysisContext context)
		{
			var expressionType = variantValueExpression.GetResultType(context);
			return expressionType == TypeDefinition.Unknown
			? TypeDefinition.Decimal
			: expressionType;
		}

		public override double GetValue(RenderContext renderContext)
		{
			return Convert.ToDouble(variantValueExpression.Evaluate(renderContext).GetPrimitiveValue());
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			variantValueExpression.PerformSemanticAnalysis(context, TypeDefinition.Decimal);
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			return variantValueExpression.CheckIfExpressionIsNull(renderContext);
		}

		public override ExpressionDTO GetTreeDTO()
		{
			return variantValueExpression.GetTreeDTO();
		}
	}
}
