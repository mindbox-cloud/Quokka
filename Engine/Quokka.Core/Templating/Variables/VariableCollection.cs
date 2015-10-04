using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Quokka
{
	internal class VariableCollection
	{
		private readonly Dictionary<string, VariableDefinition> variables;

		public VariableCollection()
			: this(new Dictionary<string, VariableDefinition>(StringComparer.InvariantCultureIgnoreCase))
		{
		}

		private VariableCollection(Dictionary<string, VariableDefinition> variables)
		{
			this.variables = variables;
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

		public static VariableCollection Merge(IEnumerable<VariableCollection> collections)
		{
			var fields = collections
				.SelectMany(fieldCollection => fieldCollection.variables.Values)
				.GroupBy(
					field => field.Name,
					(key, values) => VariableDefinition.Merge(values.ToList()),
					StringComparer.InvariantCultureIgnoreCase)
				.ToDictionary(definition => definition.Name, StringComparer.InvariantCultureIgnoreCase);

			return new VariableCollection(fields);
		}
	}
}
