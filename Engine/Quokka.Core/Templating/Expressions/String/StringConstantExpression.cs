namespace Mindbox.Quokka
{
	internal class StringConstantExpression : StringExpression
	{
		private readonly string stringValue;

		public override TypeDefinition GetResultType(SemanticAnalysisContext context)
		{
			return TypeDefinition.String;
		}

		public StringConstantExpression(string stringValue)
		{
			this.stringValue = stringValue;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context, TypeDefinition expectedExpressionType)
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
	}
}