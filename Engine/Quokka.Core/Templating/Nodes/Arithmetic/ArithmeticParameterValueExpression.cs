namespace Quokka
{
	internal class ArithmeticParameterValueExpression : ArithmeticExpressionBase
	{
		private readonly VariableOccurence variableOccurence;

		public ArithmeticParameterValueExpression(VariableOccurence variableOccurence)
		{
			this.variableOccurence = variableOccurence;
		}

		public override double GetValue(RenderContext renderContext)
		{
			return renderContext.VariableScope.GetVariableValue<int>(variableOccurence);
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			context.VariableScope.CreateOrUpdateVariableDefinition(variableOccurence);
		}
	}
}
