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

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class AdditionalExpressionVisitor : QuokkaBaseVisitor<AdditionOperand>
	{
		public AdditionalExpressionVisitor(VisitingContext visitingContext)
			: base(visitingContext)
		{
		}

		public override AdditionOperand VisitPlusOperand(QuokkaParser.PlusOperandContext context)
		{
			return AdditionOperand.Plus(context.multiplicationExpression()
				.Accept(new ArithmeticExpressionVisitor(VisitingContext)));
		}

		public override AdditionOperand VisitMinusOperand(QuokkaParser.MinusOperandContext context)
		{
			return AdditionOperand.Minus(context.multiplicationExpression()
				.Accept(new ArithmeticExpressionVisitor(VisitingContext)));
		}
	}
}
