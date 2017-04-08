namespace Mindbox.Quokka
{
	internal class NotExpression : BooleanExpressionBase
	{
		private readonly IBooleanExpression inner;

		public NotExpression(IBooleanExpression inner)
		{
			this.inner = inner;
		}

		public override bool Evaluate(RenderContext renderContext)
		{
			return !inner.Evaluate(renderContext);
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			inner.CompileVariableDefinitions(context);
		}
	}
}
