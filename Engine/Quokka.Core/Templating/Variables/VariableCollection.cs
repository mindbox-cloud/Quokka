using System.Collections.Generic;

namespace Quokka
{
	internal class VariableCollection
	{
		private readonly Dictionary<string, VariableDefinition> variables;

		public VariableCollection()
		{
			variables = new Dictionary<string, VariableDefinition>();
		}

		public bool CheckIfVariableExists(string variableName)
		{
			return variables.ContainsKey(variableName);
		}

		public VariableDefinition CreateOrUpdateVariableDefinition(
			VariableOccurence variableOccurence,
			ISemanticErrorListener errorListener)
		{
			return CreateOrUpdateVariableDefinition(variableOccurence, errorListener, null);
		}

		private VariableDefinition CreateOrUpdateVariableDefinition(
			VariableOccurence variableOccurence,
			ISemanticErrorListener errorListener,
			string fullNamePrefix)
		{
			VariableDefinition definition;
			if (variables.TryGetValue(variableOccurence.Name, out definition))
			{
				if (definition.Type != variableOccurence.RequiredType)
					errorListener.AddInconsistentVariableTypesError(definition, variableOccurence);
			}
			else
			{
				definition = new VariableDefinition(
					variableOccurence.Name,
					$"{fullNamePrefix}{variableOccurence.Name}",
					variableOccurence.RequiredType);
				variables.Add(variableOccurence.Name, definition);
			}

			if (variableOccurence.Member != null)
			{
				return definition.Fields.CreateOrUpdateVariableDefinition(
					variableOccurence.Member,
					errorListener,
					$"{fullNamePrefix}{variableOccurence.Name}.");
			}
			else
			{
				return definition;
			}
		}
	}
}
