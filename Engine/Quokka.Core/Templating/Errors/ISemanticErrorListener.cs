using System.Collections.Generic;

namespace Quokka
{
	internal interface ISemanticErrorListener
	{
		void AddInconsistentVariableTypesError(VariableDefinition definition, VariableOccurence occurence);

		IReadOnlyCollection<Error> GetErrors();
	}
}