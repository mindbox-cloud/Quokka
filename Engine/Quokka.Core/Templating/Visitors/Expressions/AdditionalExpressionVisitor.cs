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
