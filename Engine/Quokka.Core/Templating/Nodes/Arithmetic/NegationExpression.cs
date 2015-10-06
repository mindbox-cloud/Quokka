namespace Quokka
{
	internal class NegationExpression : ArithmeticExpressionBase
	{
		private readonly IArithmeticExpression innerExpression;

		public NegationExpression(IArithmeticExpression innerExpression)
		{
			this.innerExpression = innerExpression;
		}

		public override double GetValue(VariableValueStorage variableValueStorage)
		{
			return -1.0 * innerExpression.GetValue(variableValueStorage);
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			innerExpression.CompileVariableDefinitions(scope, errorListener);
		}
	}
}
