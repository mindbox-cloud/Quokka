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

namespace Mindbox.Quokka
{
	internal class NegationExpression : ArithmeticExpression
	{
		private readonly ArithmeticExpression innerExpression;

		public override TypeDefinition GetResultType(AnalysisContext context)
		{
			return innerExpression.GetResultType(context);
		}

		public NegationExpression(ArithmeticExpression innerExpression)
		{
			this.innerExpression = innerExpression;
		}

		public override double GetValue(RenderContext renderContext)
		{
			return -1.0 * innerExpression.GetValue(renderContext);
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			innerExpression.PerformSemanticAnalysis(context);
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			return innerExpression.CheckIfExpressionIsNull(renderContext);
		}

		public override ExpressionDTO GetTreeDTO()
		{
			var dto = base.GetTreeDTO();
			dto.type = "NegationExpression";
			dto.members.Add(innerExpression.GetTreeDTO());
			return dto;
		}
	}
}
