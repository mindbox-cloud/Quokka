namespace Mindbox.Quokka
{
	internal abstract class MultiplicationOperand
	{
		public ArithmeticExpression Expression { get; }

		protected MultiplicationOperand(ArithmeticExpression expression)
		{
			Expression = expression;
		}

		public abstract double Calculate(double leftOperand, RenderContext renderContext);

		public static MultiplicationOperand Multiply(ArithmeticExpression expression)
		{
			return new MultOperand(expression);
		}

		public static MultiplicationOperand Divide(ArithmeticExpression expression)
		{
			return new DivOperand(expression);
		}
		
		private class MultOperand : MultiplicationOperand
		{
			public MultOperand(ArithmeticExpression expression)
				: base(expression)
			{
			}

			public override double Calculate(double leftOperand, RenderContext renderContext)
			{
				return leftOperand * Expression.GetValue(renderContext);
			}
		}

		private class DivOperand : MultiplicationOperand
		{
			public DivOperand(ArithmeticExpression expression)
				: base(expression)
			{
			}

			public override double Calculate(double leftOperand, RenderContext renderContext)
			{
				return leftOperand / Expression.GetValue(renderContext);
			}
		}
	}
}
