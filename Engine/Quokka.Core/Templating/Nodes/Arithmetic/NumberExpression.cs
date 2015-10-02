namespace Quokka
{
	internal class NumberExpression : IArithmeticExpression
	{
		private readonly double number;

		public NumberExpression(double number)
		{
			this.number = number;
		}

		public double GetValue()
		{
			return number;
		}
	}
}
