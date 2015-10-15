namespace Quokka
{
	internal class BooleanParameterValueExpression : BooleanExpressionBase
	{
		private readonly VariableOccurence variableOccurence;

		public BooleanParameterValueExpression(VariableOccurence variableOccurence)
		{
			this.variableOccurence = variableOccurence;
		}

		public override bool Evaluate(RenderContext renderContext)
		{
			var value = renderContext.VariableScope.GetVariableValue<bool>(variableOccurence);
			return value;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			context.VariableScope.CreateOrUpdateVariableDefinition(variableOccurence);
		}
	}
}
