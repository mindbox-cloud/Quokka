using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Quokka
{
	internal class VariableCollection
	{
		private readonly Dictionary<string, VariableDefinition> variables;

		public VariableCollection()
		{
			variables = new Dictionary<string, VariableDefinition>();
		}

		public ReadOnlyCollection<IParameterDefinition> GetParameterDefinitions()
		{
			return variables
				.Select(kvp => kvp.Value.ToParameterDefinition())
				.OrderBy(parameter => parameter.Name)
				.ToList()
				.AsReadOnly();
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
			return definition;
		}
	}
}
