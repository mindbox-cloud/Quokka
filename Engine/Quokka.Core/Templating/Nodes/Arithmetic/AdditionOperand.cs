namespace Quokka
{
	internal abstract class AdditionOperand
	{
		private readonly IArithmeticExpression expression;

		protected AdditionOperand(IArithmeticExpression expression)
		{
			this.expression = expression;
		}

		public abstract double Calculate(double leftOperand);

		public static AdditionOperand Plus(IArithmeticExpression expression)
		{
			return new PlusOperand(expression);
		}

		public static AdditionOperand Minus(IArithmeticExpression expression)
		{
			return new MinusOperand(expression);
		}
		
		private class PlusOperand : AdditionOperand
		{
			public PlusOperand(IArithmeticExpression expression)
				: base(expression)
			{
			}

			public override double Calculate(double leftOperand)
			{
				return leftOperand + expression.GetValue();
			}
		}

		private class MinusOperand : AdditionOperand
		{
			public MinusOperand(IArithmeticExpression expression)
				: base(expression)
			{
			}

			public override double Calculate(double leftOperand)
			{
				return leftOperand - expression.GetValue();
			}
		}
	}
}
