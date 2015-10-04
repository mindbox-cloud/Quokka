using System.Linq;

namespace Quokka
{
	internal class BooleanParameterValueExpression : BooleanExpressionBase
	{
		private readonly VariableOccurence variableOccurence;

		public BooleanParameterValueExpression(VariableOccurence variableOccurence)
		{
			this.variableOccurence = variableOccurence;
		}

		public override bool Evaluate()
		{
			// TODO: this is very temporary and should be removed. 
			// For now we consider the parameter to always be True if it's a complex parameter (member access), otherwise False.
			return variableOccurence.Member != null;
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			scope.CreateOrUpdateVariableDefinition(variableOccurence, errorListener);
		}
	}
}
