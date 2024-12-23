﻿// // Copyright 2022 Mindbox Ltd
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
	internal class AdditionExpression : ArithmeticExpression
	{
		private readonly IReadOnlyCollection<AdditionOperand> operands;
		
		public override TypeDefinition GetResultType(AnalysisContext context)
		{
			return operands.All(op => op.Expression.GetResultType(context) == TypeDefinition.Integer)
						? TypeDefinition.Integer
						: TypeDefinition.Decimal;
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			return operands.Any(operand => operand.Expression.CheckIfExpressionIsNull(renderContext));
		}

		public override void Accept(ITemplateVisitor treeVisitor)
		{
			treeVisitor.VisitAdditionExpression();

			foreach (var operand in operands)
				operand.Accept(treeVisitor);
			
			treeVisitor.EndVisit();
		}

		public AdditionExpression(IEnumerable<AdditionOperand> operands)
		{
			this.operands = operands.ToList().AsReadOnly();
		}

		public override double GetValue(RenderContext renderContext)
		{
			return operands
				.Aggregate(0.0, (current, operand) => operand.Calculate(current, renderContext));
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			foreach (var operand in operands)
				operand.Expression.PerformSemanticAnalysis(context);
		}
	}
}
