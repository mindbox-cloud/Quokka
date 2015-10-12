namespace Quokka
{
	internal class NegationExpression : ArithmeticExpressionBase
	{
		private readonly IArithmeticExpression innerExpression;

		public NegationExpression(IArithmeticExpression innerExpression)
		{
			this.innerExpression = innerExpression;
		}

		public override double GetValue(RenderContext renderContext)
		{
			return -1.0 * innerExpression.GetValue(renderContext);
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			innerExpression.CompileVariableDefinitions(context);
		}
	}
}
