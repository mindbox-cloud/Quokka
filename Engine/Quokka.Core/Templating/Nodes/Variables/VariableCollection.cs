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

		public void ProcessVariableOccurence(
			VariableOccurence variableOccurence,
			ISemanticErrorListener errorListener)
		{
			ProcessVariableOccurence(variableOccurence, errorListener, null);
		}

		private void ProcessVariableOccurence(
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
				definition.Fields.ProcessVariableOccurence(
					variableOccurence.Member,
					errorListener,
					$"{fullNamePrefix}{variableOccurence.Name}.");
			}
		}
	}
}
