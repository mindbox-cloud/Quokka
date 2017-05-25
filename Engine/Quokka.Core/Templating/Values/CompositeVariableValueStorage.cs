using System;
using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
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
					field => field.Value != null ? CreateStorageForValue(field.Value) : null,
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
		
		public bool ContainsValueForVariable(string variableName)
		{
			return fields.ContainsKey(variableName);
		}

		public override VariableValueStorage GetMemberValueStorage(string memberName)
		{
			if (fields.TryGetValue(memberName, out VariableValueStorage field))
				return field;
			else
				throw new InvalidOperationException($"Member {memberName} not found");
		}
	}
}