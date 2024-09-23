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

using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	internal class MultiplicationExpression : ArithmeticExpression
	{
		public readonly IReadOnlyCollection<MultiplicationOperand> operands;

		public override TypeDefinition GetResultType(AnalysisContext context)
		{
			return operands.All(op => op.Expression.GetResultType(context) == TypeDefinition.Integer)
						? TypeDefinition.Integer
						: TypeDefinition.Decimal;
		}

		public MultiplicationExpression(IEnumerable<MultiplicationOperand> operands)
		{
			this.operands = operands.ToList().AsReadOnly();
		}

		public override double GetValue(RenderContext renderContext)
		{
			return operands
				.Aggregate(1.0, (current, operand) => operand.Calculate(current, renderContext));
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			foreach (var operand in operands)
				operand.Expression.PerformSemanticAnalysis(context);
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			return operands.Any(operand => operand.Expression.CheckIfExpressionIsNull(renderContext));
		}

		public override ExpressionDTO GetTreeDTO()
		{
			var dto = base.GetTreeDTO();
			dto.type = "Multiplication";
			dto.members = operands.Select(op => op.Expression.GetTreeDTO()).ToList();
			return dto;
		}
	}
}
