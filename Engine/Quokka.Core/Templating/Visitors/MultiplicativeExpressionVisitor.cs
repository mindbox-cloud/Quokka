using Quokka.Generated;

namespace Quokka
{
	internal class MultiplicativeExpressionVisitor : QuokkaBaseVisitor<MultiplicationOperand>
	{
		public static MultiplicativeExpressionVisitor Instance { get; } = new MultiplicativeExpressionVisitor();

		private MultiplicativeExpressionVisitor()
		{
		}

		public override MultiplicationOperand VisitMultiplicationOperand(QuokkaParser.MultiplicationOperandContext context)
		{
			return MultiplicationOperand.Multiply(context.arithmeticAtom().Accept(ArithmeticExpressionVisitor.Instance));
		}

		public override MultiplicationOperand VisitDivisionOperand(QuokkaParser.DivisionOperandContext context)
		{
			return MultiplicationOperand.Divide(context.arithmeticAtom().Accept(ArithmeticExpressionVisitor.Instance));
		}
	}
}
