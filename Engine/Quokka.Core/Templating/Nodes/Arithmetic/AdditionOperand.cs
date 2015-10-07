namespace Quokka
{
	internal abstract class AdditionOperand
	{
		public IArithmeticExpression Expression { get; }

		protected AdditionOperand(IArithmeticExpression expression)
		{
			Expression = expression;
		}

		public abstract double Calculate(double leftOperand, RuntimeVariableScope variableScope);

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

			public override double Calculate(double leftOperand, RuntimeVariableScope variableScope)
			{
				return leftOperand + Expression.GetValue(variableScope);
			}
		}

		private class MinusOperand : AdditionOperand
		{
			public MinusOperand(IArithmeticExpression expression)
				: base(expression)
			{
			}

			public override double Calculate(double leftOperand, RuntimeVariableScope variableScope)
			{
				return leftOperand - Expression.GetValue(variableScope);
			}
		}
	}
}
