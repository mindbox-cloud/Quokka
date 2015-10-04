namespace Quokka
{
	internal class ArithmeticParameterValueExpression : ArithmeticExpressionBase
	{
		private readonly VariableOccurence variableOccurence;

		public ArithmeticParameterValueExpression(VariableOccurence variableOccurence)
		{
			this.variableOccurence = variableOccurence;
		}

		public override double GetValue()
		{
			// TODO: this is very temporary and should be removed. 
			// For now we consider the parameter to always be True if it's a complex parameter (member access), otherwise False.
			return variableOccurence.Name.Length;
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			scope.CreateOrUpdateVariableDefinition(variableOccurence, errorListener);
		}
	}
}
