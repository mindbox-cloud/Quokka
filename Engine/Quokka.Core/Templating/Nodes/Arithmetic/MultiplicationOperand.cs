namespace Quokka
{
	internal abstract class MultiplicationOperand
	{
		private readonly IArithmeticExpression expression;

		protected MultiplicationOperand(IArithmeticExpression expression)
		{
			this.expression = expression;
		}

		public abstract double Calculate(double leftOperand);

		public static MultiplicationOperand Multiply(IArithmeticExpression expression)
		{
			return new MultOperand(expression);
		}

		public static MultiplicationOperand Divide(IArithmeticExpression expression)
		{
			return new DivOperand(expression);
		}
		
		private class MultOperand : MultiplicationOperand
		{
			public MultOperand(IArithmeticExpression expression)
				: base(expression)
			{
			}

			public override double Calculate(double leftOperand)
			{
				return leftOperand * expression.GetValue();
			}
		}

		private class DivOperand : MultOperand
		{
			public DivOperand(IArithmeticExpression expression)
				: base(expression)
			{
			}

			public override double Calculate(double leftOperand)
			{
				return leftOperand / expression.GetValue();
			}
		}
	}
}
