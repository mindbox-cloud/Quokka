namespace Mindbox.Quokka
{
	internal class NumberExpression : ArithmeticExpression
	{
		private readonly double number;

		public NumberExpression(double number)
		{
			this.number = number;
		}

		public override TypeDefinition Type => TypeDefinition.Integer;

		public override double GetValue(RenderContext renderContext)
		{
			return number;
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			// This node is constant and therefore can't affect semantic analysis context.
		}
		
		protected override bool TryGetStaticValue(out double value)
		{
			value = number;
			return true;
		}
	}
}
