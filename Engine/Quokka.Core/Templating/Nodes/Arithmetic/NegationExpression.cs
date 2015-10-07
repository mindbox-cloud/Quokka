namespace Quokka
{
	internal class NegationExpression : ArithmeticExpressionBase
	{
		private readonly IArithmeticExpression innerExpression;

		public NegationExpression(IArithmeticExpression innerExpression)
		{
			this.innerExpression = innerExpression;
		}

		public override double GetValue(RuntimeVariableScope variableScope)
		{
			return -1.0 * innerExpression.GetValue(variableScope);
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			innerExpression.CompileVariableDefinitions(scope, errorListener);
		}
	}
}
