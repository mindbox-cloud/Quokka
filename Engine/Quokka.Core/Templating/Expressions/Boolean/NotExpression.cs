namespace Mindbox.Quokka
{
	internal class NotExpression : BooleanExpression
	{
		private readonly BooleanExpression inner;

		public NotExpression(BooleanExpression inner)
		{
			this.inner = inner;
		}

		public override bool GetBooleanValue(RenderContext renderContext)
		{
			return !inner.GetBooleanValue(renderContext);
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			inner.PerformSemanticAnalysis(context);
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			return inner.CheckIfExpressionIsNull(renderContext);
		}
	}
}
