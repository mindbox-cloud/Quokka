namespace Mindbox.Quokka
{
	internal class TrueExpression : BooleanExpression
	{
		public override bool GetBooleanValue(RenderContext renderContext)
		{
			return true;
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			// This node is constant and therefore can't affect semantic analysis context.
		}
	}
}
