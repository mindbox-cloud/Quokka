namespace Mindbox.Quokka
{
	internal class StringConstantExpression : StringExpression
	{
		private readonly string stringValue;

		public StringConstantExpression(string stringValue)
		{
			this.stringValue = stringValue;
		}

		public override void PerformSemanticAnalysis(AnalysisContext context, TypeDefinition expectedExpressionType)
		{
		}

		public override VariableValueStorage TryGetStaticEvaluationResult()
		{
			return new PrimitiveVariableValueStorage(stringValue);
		}

		public override VariableValueStorage Evaluate(RenderContext renderContext)
		{
			return new PrimitiveVariableValueStorage(stringValue);
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			return false;
		}
	}
}