namespace Quokka
{
	internal class NumberExpression : ArithmeticExpressionBase
	{
		private readonly double number;

		public NumberExpression(double number)
		{
			this.number = number;
		}

		public override double GetValue(RuntimeVariableScope variableScope)
		{
			return number;
		}
	}
}
