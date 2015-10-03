namespace Quokka
{
	internal interface ISemanticErrorListener
	{
		void AddInconsistentVariableTypesError(VariableDefinition definition, VariableOccurence occurence);
	}
}