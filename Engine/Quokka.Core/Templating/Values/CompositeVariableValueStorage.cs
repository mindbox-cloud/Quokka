using System;
using System.Collections.Generic;
using System.Linq;

using Quokka.Templating;

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

		public bool ContainsValueForVariable(VariableOccurence variableOccurence)
		{
			return fields.ContainsKey(variableOccurence.Name);
		}

		public VariableValueStorage GetValueStorageForVariable(VariableOccurence variableOccurence)
		{
			return fields[variableOccurence.Name];
		}

		/// <summary>
		/// Get the value storage for the leaf member of potentially multi-part identifier. For example, for variable
		/// Product.Details.Description
		/// this method will return the storage that contains the value for the rightmost Description field.
		/// </summary>
		/// <param name="variableOccurence">Variable occurence</param>
		/// <returns>Will return the storage for value or <c>null</c> if the .</returns>
		public override VariableValueStorage GetLeafMemberValueStorage(VariableOccurence variableOccurence)
		{
			VariableValueStorage field;
			if (fields.TryGetValue(variableOccurence.Name, out field))
			{
				if (variableOccurence.Member == null)
				{
					return field;
				}
				else
				{
					if (field == null)
						throw new ValueStorageAccessException("Value storage for field is null", variableOccurence);

					return field.GetLeafMemberValueStorage(variableOccurence.Member ?? variableOccurence);
				}
			}
			else
				throw new InvalidOperationException($"Field {variableOccurence.Name} not found");
		}
	}
}