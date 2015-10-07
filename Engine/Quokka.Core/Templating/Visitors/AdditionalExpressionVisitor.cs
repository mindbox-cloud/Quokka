using Quokka.Generated;

namespace Quokka
{
	internal class AdditionalExpressionVisitor : QuokkaBaseVisitor<AdditionOperand>
	{
		public static AdditionalExpressionVisitor Instance { get; } = new AdditionalExpressionVisitor();

		private AdditionalExpressionVisitor()
		{
		}

		public override AdditionOperand VisitPlusOperand(QuokkaParser.PlusOperandContext context)
		{
			return AdditionOperand.Plus(context.multiplicationExpression().Accept(ArithmeticExpressionVisitor.Instance));
		}

		public override AdditionOperand VisitMinusOperand(QuokkaParser.MinusOperandContext context)
		{
			return AdditionOperand.Minus(context.multiplicationExpression().Accept(ArithmeticExpressionVisitor.Instance));
		}
	}
}
