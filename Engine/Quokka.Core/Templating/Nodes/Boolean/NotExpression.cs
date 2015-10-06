namespace Quokka
{
	internal class NotExpression : BooleanExpressionBase
	{
		private readonly IBooleanExpression inner;

		public NotExpression(IBooleanExpression inner)
		{
			this.inner = inner;
		}

		public override bool Evaluate(VariableValueStorage valueStorage)
		{
			return !inner.Evaluate(valueStorage);
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			inner.CompileVariableDefinitions(scope, errorListener);
		}
	}
}
