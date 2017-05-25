namespace Mindbox.Quokka
{
	internal class NumberExpression : ArithmeticExpressionBase
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

		public override bool TryGetStaticValue(out object value)
		{
			value = number;
			return true;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			// This node is constant and therefore can't affect semantic analysis context.
		}
	}
}
