namespace Mindbox.Quokka
{
	internal class NotExpression : BooleanExpressionBase
	{
		private readonly IBooleanExpression inner;

		public NotExpression(IBooleanExpression inner)
		{
			this.inner = inner;
		}

		public override bool GetBooleanValue(RenderContext renderContext)
		{
			return !inner.GetBooleanValue(renderContext);
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			inner.CompileVariableDefinitions(context);
		}
	}
}
