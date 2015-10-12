using System.Collections.Generic;

namespace Quokka
{
	internal interface ISemanticErrorListener
	{
		void AddInconsistentVariableTypingError(
			VariableDefinition definition,
			VariableOccurence faultyOccurence,
			VariableType correctType);

		void AddUndefinedFunctionError(
			string functionName,
			Location location);

		void AddInvalidFunctionArgumentValueError(
			string functionName,
			string argumentName,
			string message,
			Location location);

		IReadOnlyCollection<SemanticError> GetErrors();
	}
}