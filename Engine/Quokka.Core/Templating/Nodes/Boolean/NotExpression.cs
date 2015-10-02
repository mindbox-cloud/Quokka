namespace Quokka
{
	internal class NotExpression : IBooleanExpression
	{
		private readonly IBooleanExpression inner;

		public NotExpression(IBooleanExpression inner)
		{
			this.inner = inner;
		}

		public bool Evaluate()
		{
			return !inner.Evaluate();
		}
	}
}
