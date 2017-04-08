using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class MultiplicativeExpressionVisitor : QuokkaBaseVisitor<MultiplicationOperand>
	{
		public MultiplicativeExpressionVisitor(VisitingContext visitingContext)
			: base(visitingContext)
		{
		}

		public override MultiplicationOperand VisitMultiplicationOperand(QuokkaParser.MultiplicationOperandContext context)
		{
			return MultiplicationOperand.Multiply(context.arithmeticAtom()
				.Accept(new ArithmeticExpressionVisitor(visitingContext)));
		}

		public override MultiplicationOperand VisitDivisionOperand(QuokkaParser.DivisionOperandContext context)
		{
			return MultiplicationOperand.Divide(context.arithmeticAtom()
				.Accept(new ArithmeticExpressionVisitor(visitingContext)));
		}
	}
}
