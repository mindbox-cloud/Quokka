namespace Quokka
{
	internal class NegationExpression : IArithmeticExpression
	{
		private readonly IArithmeticExpression innerExpression;

		public NegationExpression(IArithmeticExpression innerExpression)
		{
			this.innerExpression = innerExpression;
		}

		public double GetValue()
		{
			return -1.0 * innerExpression.GetValue();
		}
	}
}
