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
	internal class StringConcatenationExpression : StringExpression
	{
		private readonly IExpression firstOperand;
		private readonly IExpression secondOperand;

		public StringConcatenationExpression(
			IExpression firstOperand,
			IExpression secondOperand)
		{
			if (firstOperand == null)
				throw new ArgumentNullException(nameof(firstOperand));
			if (secondOperand == null)
				throw new ArgumentNullException(nameof(secondOperand));

			this.firstOperand = firstOperand;
			this.secondOperand = secondOperand;
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			return false;
		}

		public override VariableValueStorage Evaluate(RenderContext renderContext)
		{
			return new PrimitiveVariableValueStorage(
				firstOperand.GetOutputValue(renderContext) +
				secondOperand.GetOutputValue(renderContext));
		}

		public override void PerformSemanticAnalysis(AnalysisContext context, TypeDefinition expectedExpressionType)
		{
			firstOperand.PerformSemanticAnalysis(context, TypeDefinition.Primitive);
			secondOperand.PerformSemanticAnalysis(context, TypeDefinition.Primitive);
		}

		public override VariableValueStorage TryGetStaticEvaluationResult()
		{
			return null;
		}

		public override ExpressionDTO GetTreeDTO()
		{
			var dto = base.GetTreeDTO();
			dto.members = new List<ExpressionDTO>
			{
				firstOperand.GetTreeDTO(),
				secondOperand.GetTreeDTO()
			};
			return dto;
		}
	}
}
