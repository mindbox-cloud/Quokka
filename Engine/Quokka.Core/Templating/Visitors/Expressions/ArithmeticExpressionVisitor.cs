using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class ArithmeticExpressionVisitor : QuokkaBaseVisitor<IArithmeticExpression>
	{
		public ArithmeticExpressionVisitor(VisitingContext visitingContext)
			: base(visitingContext)
		{
		}

		public override IArithmeticExpression VisitArithmeticExpression(QuokkaParser.ArithmeticExpressionContext context)
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

		public override IArithmeticExpression VisitMultiplicationExpression(QuokkaParser.MultiplicationExpressionContext context)
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

		public override IArithmeticExpression VisitNegationExpression(QuokkaParser.NegationExpressionContext context)
		{
			return new NegationExpression(Visit(context.arithmeticAtom()));
		}

		public override IArithmeticExpression VisitArithmeticAtom(QuokkaParser.ArithmeticAtomContext context)
		{
			var number = context.Number();
			if (number != null)
				return new NumberExpression(double.Parse(number.GetText(), CultureInfo.InvariantCulture));

			return base.VisitArithmeticAtom(context);
		}

		public override IArithmeticExpression VisitVariantValueExpression(QuokkaParser.VariantValueExpressionContext context)
		{
			return new VariantValueArithmeticExpression(context.Accept(new VariantValueExpressionVisitor(VisitingContext)));
		}

		protected override IArithmeticExpression AggregateResult(IArithmeticExpression aggregate, IArithmeticExpression nextResult)
		{
			return aggregate ?? nextResult;
		}
	}
}
