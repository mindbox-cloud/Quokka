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

		IReadOnlyCollection<SemanticError> GetErrors();
	}
}