using Quokka.Generated;

namespace Quokka
{
	internal class MultiplicativeExpressionVisitor : QuokkaBaseVisitor<MultiplicationOperand>
	{
		public override MultiplicationOperand VisitMultiplicationOperand(QuokkaParser.MultiplicationOperandContext context)
		{
			return MultiplicationOperand.Multiply(context.arithmeticAtom().Accept(new ArithmeticExpressionVisitor()));
		}

		public override MultiplicationOperand VisitDivisionOperand(QuokkaParser.DivisionOperandContext context)
		{
			return MultiplicationOperand.Divide(context.arithmeticAtom().Accept(new ArithmeticExpressionVisitor()));
		}
	}
}
