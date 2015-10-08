namespace Quokka
{
	internal class BooleanParameterValueExpression : BooleanExpressionBase
	{
		private readonly VariableOccurence variableOccurence;

		public BooleanParameterValueExpression(VariableOccurence variableOccurence)
		{
			this.variableOccurence = variableOccurence;
		}

		public override bool Evaluate(RuntimeVariableScope variableScope)
		{
			var value = (bool)variableScope.GetVariableValue(variableOccurence);
			return value;
		}

		public override void CompileVariableDefinitions(CompilationVariableScope scope)
		{
			scope.CreateOrUpdateVariableDefinition(variableOccurence);
		}
	}
}
