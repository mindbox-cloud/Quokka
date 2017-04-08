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
				return Visit(context.multiplicationExpression());

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

			// Optimization: we can use single operand of multipart-expression itself without polluting the tree with
			// additional layer of arithmetic nodes.
			if (!context.multiplicationOperand().Any() && !context.divisionOperand().Any())
				return Visit(context.arithmeticAtom());

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
				return new NumberExpression(double.Parse(number.GetText(), CultureInfo.InvariantCulture));

			return base.VisitArithmeticAtom(context);
		}

		public override IArithmeticExpression VisitParameterValueExpression(QuokkaParser.ParameterValueExpressionContext context)
		{
			return new ArithmeticParameterValueExpression(
				context.Accept(new VariableVisitor(visitingContext, TypeDefinition.Decimal)));
		}

		public override IArithmeticExpression VisitFunctionCall(QuokkaParser.FunctionCallContext context)
		{
			return new ArithmeticFunctionCallExpression(
				context.Accept(new FunctionCallVisitor(visitingContext)));
		}

		protected override IArithmeticExpression AggregateResult(IArithmeticExpression aggregate, IArithmeticExpression nextResult)
		{
			return aggregate ?? nextResult;
		}
	}
}
