namespace Quokka
{
	internal class BooleanParameterValueExpression : BooleanExpressionBase
	{
		private readonly VariableOccurence variableOccurence;

		public BooleanParameterValueExpression(VariableOccurence variableOccurence)
		{
			this.variableOccurence = variableOccurence;
		}

		public override bool Evaluate(VariableValueStorage valueStorage)
		{
			var value = (bool)valueStorage.GetValue(variableOccurence);
			return value;
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			scope.CreateOrUpdateVariableDefinition(variableOccurence, errorListener);
		}
	}
}
