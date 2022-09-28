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
using System.Globalization;
using System.Linq;

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class ArithmeticExpressionVisitor : QuokkaBaseVisitor<ArithmeticExpression>
	{
		public ArithmeticExpressionVisitor(VisitingContext visitingContext)
			: base(visitingContext)
		{
		}

		public override ArithmeticExpression VisitArithmeticExpression(QuokkaParser.ArithmeticExpressionContext context)
		{
			// Optimization: we can use single operand of multipart-expression itself without polluting the tree with
			// additional layer of arithmetic nodes.

			if (!context.minusOperand().Any() && !context.plusOperand().Any())
				return context.multiplicationExpression().Accept(this);

			var operands = new List<AdditionOperand>
			{
				AdditionOperand.Plus(context.multiplicationExpression().Accept(this))
			};

			operands.AddRange(context
				.children
				.Skip(1)
				.Select(child => child.Accept(new AdditionalExpressionVisitor(VisitingContext))));

			return new AdditionExpression(operands);
		}

		public override ArithmeticExpression VisitMultiplicationExpression(QuokkaParser.MultiplicationExpressionContext context)
		{

			// Optimization: we can use single operand of multipart-expression itself without polluting the tree with
			// additional layer of arithmetic nodes.

			if (!context.multiplicationOperand().Any() && !context.divisionOperand().Any())
				return context.arithmeticAtom().Accept(this);

			var operands = new List<MultiplicationOperand>
			{
				MultiplicationOperand.Multiply(context.arithmeticAtom().Accept(this))
			};

			operands.AddRange(context
				.children
				.Skip(1)
				.Select(child => child.Accept(new MultiplicativeExpressionVisitor(VisitingContext))));

			return new MultiplicationExpression(operands);
		}

		public override ArithmeticExpression VisitNegationExpression(QuokkaParser.NegationExpressionContext context)
		{
			return new NegationExpression(Visit(context.arithmeticAtom()));
		}

		public override ArithmeticExpression VisitArithmeticAtom(QuokkaParser.ArithmeticAtomContext context)
		{
			var number = context.Number();
			if (number != null)
				return new NumberExpression(double.Parse(number.GetText(), CultureInfo.InvariantCulture));

			return base.VisitArithmeticAtom(context);
		}

		public override ArithmeticExpression VisitVariantValueExpression(QuokkaParser.VariantValueExpressionContext context)
		{
			return new VariantValueArithmeticExpression(context.Accept(new VariantValueExpressionVisitor(VisitingContext)));
		}

		protected override ArithmeticExpression AggregateResult(ArithmeticExpression aggregate, ArithmeticExpression nextResult)
		{
			return aggregate ?? nextResult;
		}
	}
}
