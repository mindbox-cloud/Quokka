﻿using System.Collections.Generic;
using System.Linq;

using Quokka.Generated;

namespace Quokka
{
	internal class ArithmeticExpressionVisitor : QuokkaBaseVisitor<IArithmeticExpression>
	{
		public ArithmeticExpressionVisitor(VisitingContext visitingContext)
			: base(visitingContext)
		{
		}

		public override IArithmeticExpression VisitArithmeticExpression(QuokkaParser.ArithmeticExpressionContext context)
		{
			var operands = new List<AdditionOperand>
			{
				AdditionOperand.Plus(Visit(context.multiplicationExpression()))
			};
			operands.AddRange(context
				.children
				.Skip(1)
				.Select(child => child.Accept(new AdditionalExpressionVisitor(visitingContext))));
			return new AdditionExpression(operands);
		}

		public override IArithmeticExpression VisitMultiplicationExpression(QuokkaParser.MultiplicationExpressionContext context)
		{
			var operands = new List<MultiplicationOperand>
			{
				MultiplicationOperand.Multiply(Visit(context.arithmeticAtom()))
			};
			operands.AddRange(context
				.children
				.Skip(1)
				.Select(child => child.Accept(new MultiplicativeExpressionVisitor(visitingContext))));
			return new MultiplicationExpression(operands);
		}

		public override IArithmeticExpression VisitNegationExpression(QuokkaParser.NegationExpressionContext context)
		{
			return new NegationExpression(Visit(context.arithmeticAtom()));
		}

		public override IArithmeticExpression VisitArithmeticAtom(QuokkaParser.ArithmeticAtomContext context)
		{
			var number = context.Number();
			if (number != null)
				return new NumberExpression(double.Parse(number.GetText()));

			return base.VisitArithmeticAtom(context);
		}

		public override IArithmeticExpression VisitParameterValueExpression(QuokkaParser.ParameterValueExpressionContext context)
		{
			return new ArithmeticParameterValueExpression(
				context.Accept(new VariableVisitor(visitingContext, TypeDefinition.Integer)));
		}

		protected override IArithmeticExpression AggregateResult(IArithmeticExpression aggregate, IArithmeticExpression nextResult)
		{
			return aggregate ?? nextResult;
		}
	}
}
