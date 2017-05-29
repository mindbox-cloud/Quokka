namespace Mindbox.Quokka
{
	internal class NegationExpression : ArithmeticExpression
	{
		private readonly ArithmeticExpression innerExpression;

		public override TypeDefinition Type => innerExpression.Type;

		public NegationExpression(ArithmeticExpression innerExpression)
		{
			this.innerExpression = innerExpression;
		}

		public override double GetValue(RenderContext renderContext)
		{
			return -1.0 * innerExpression.GetValue(renderContext);
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			innerExpression.PerformSemanticAnalysis(context);
		}
	}
}
