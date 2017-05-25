namespace Mindbox.Quokka
{
	internal abstract class MultiplicationOperand
	{
		public IArithmeticExpression Expression { get; }

		protected MultiplicationOperand(IArithmeticExpression expression)
		{
			Expression = expression;
		}

		public abstract double Calculate(double leftOperand, RenderContext renderContext);

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

			public override double Calculate(double leftOperand, RenderContext renderContext)
			{
				return leftOperand * Expression.GetValue(renderContext);
			}
		}

		private class DivOperand : MultiplicationOperand
		{
			public DivOperand(IArithmeticExpression expression)
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
