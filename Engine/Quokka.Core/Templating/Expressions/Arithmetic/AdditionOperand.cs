namespace Mindbox.Quokka
{
	internal abstract class AdditionOperand
	{
		public ArithmeticExpression Expression { get; }

		protected AdditionOperand(ArithmeticExpression expression)
		{
			Expression = expression;
		}

		public abstract double Calculate(double leftOperand, RenderContext renderContext);

		public static AdditionOperand Plus(ArithmeticExpression expression)
		{
			return new PlusOperand(expression);
		}

		public static AdditionOperand Minus(ArithmeticExpression expression)
		{
			return new MinusOperand(expression);
		}
		
		private class PlusOperand : AdditionOperand
		{
			public PlusOperand(ArithmeticExpression expression)
				: base(expression)
			{
			}

			public override double Calculate(double leftOperand, RenderContext renderContext)
			{
				return leftOperand + Expression.GetValue(renderContext);
			}
		}

		private class MinusOperand : AdditionOperand
		{
			public MinusOperand(ArithmeticExpression expression)
				: base(expression)
			{
			}

			public override double Calculate(double leftOperand, RenderContext renderContext)
			{
				return leftOperand - Expression.GetValue(renderContext);
			}
		}
	}
}
