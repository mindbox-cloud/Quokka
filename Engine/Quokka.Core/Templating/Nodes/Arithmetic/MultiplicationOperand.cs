namespace Quokka
{
	internal abstract class MultiplicationOperand
	{
		public IArithmeticExpression Expression { get; }

		protected MultiplicationOperand(IArithmeticExpression expression)
		{
			this.Expression = expression;
		}

		public abstract double Calculate(double leftOperand, VariableValueStorage valueStorage);

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

			public override double Calculate(double leftOperand, VariableValueStorage valueStorage)
			{
				return leftOperand * Expression.GetValue(valueStorage);
			}
		}

		private class DivOperand : MultOperand
		{
			public DivOperand(IArithmeticExpression expression)
				: base(expression)
			{
			}

			public override double Calculate(double leftOperand, VariableValueStorage valueStorage)
			{
				return leftOperand / Expression.GetValue(valueStorage);
			}
		}
	}
}
