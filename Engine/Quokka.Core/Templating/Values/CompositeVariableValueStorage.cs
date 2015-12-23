using System;
using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class CompositeVariableValueStorage : VariableValueStorage
	{
		private readonly IDictionary<string, VariableValueStorage> fields;

		public override IModelValue ModelValue { get; }

		public CompositeVariableValueStorage(ICompositeModelValue modelValue)
		{
			if (modelValue == null)
				throw new ArgumentNullException(nameof(modelValue));

			ModelValue = modelValue;

			fields = modelValue
				.Fields
				.ToDictionary(
					field => field.Name.Trim(),
					field => CreateStorageForValue(field.Value),
					StringComparer.InvariantCultureIgnoreCase);
		}

		public CompositeVariableValueStorage(string fieldName, VariableValueStorage fieldValueStorage)
		{
			if (fieldName == null)
				throw new ArgumentNullException(nameof(fieldName));
			if (fieldValueStorage == null)
				throw new ArgumentNullException(nameof(fieldValueStorage));

			fields = new Dictionary<string, VariableValueStorage>(StringComparer.InvariantCultureIgnoreCase)
			{
				{ fieldName, fieldValueStorage }
			};
		}

		public bool ContainsValueForVariable(VariableOccurence variableOccurence)
		{
			return fields.ContainsKey(variableOccurence.Name);
		}

		public VariableValueStorage GetValueStorageForVariable(VariableOccurence variableOccurence)
		{
			return fields[variableOccurence.Name];
		}

		public override VariableValueStorage GetLeafMemberValueStorage(VariableOccurence variableOccurence)
		{
			VariableValueStorage field;
			if (fields.TryGetValue(variableOccurence.Name, out field))
				return field.GetLeafMemberValueStorage(variableOccurence.Member ?? variableOccurence);
			else
				throw new InvalidOperationException($"Field {variableOccurence.Name} not found");
		}
	}
}