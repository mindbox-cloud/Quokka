namespace Quokka
{
	internal class NotExpression : BooleanExpressionBase
	{
		private readonly IBooleanExpression inner;

		public NotExpression(IBooleanExpression inner)
		{
			this.inner = inner;
		}

		public override bool Evaluate()
		{
			return !inner.Evaluate();
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			inner.CompileVariableDefinitions(scope, errorListener);
		}
	}
}
