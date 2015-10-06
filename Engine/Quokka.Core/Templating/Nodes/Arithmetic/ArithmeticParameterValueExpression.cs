namespace Quokka
{
	internal class ArithmeticParameterValueExpression : ArithmeticExpressionBase
	{
		private readonly VariableOccurence variableOccurence;

		public ArithmeticParameterValueExpression(VariableOccurence variableOccurence)
		{
			this.variableOccurence = variableOccurence;
		}

		public override double GetValue(VariableValueStorage variableValueStorage)
		{
			return (int)variableValueStorage.GetValue(variableOccurence);
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			scope.CreateOrUpdateVariableDefinition(variableOccurence, errorListener);
		}
	}
}
