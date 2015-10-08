using System;
using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal abstract class VariableValueStorage
	{
		public abstract object TryGetValue(VariableOccurence variableOccurence);

		public static VariableValueStorage CreateCompositeStorage(string fieldName, VariableValueStorage fieldValueStorage)
		{
			return new CompositeVariableValueStorage(fieldName, fieldValueStorage);
		}

		public static VariableValueStorage CreateStorageForValue(IParameterValue value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			var primitiveValue = value as IPrimitiveParameterValue;
			if (primitiveValue != null)
				return new PrimitiveVariableValueStorage(primitiveValue.Value);

			var compositeValue = value as ICompositeParameterValue;
			if (compositeValue != null)
				return new CompositeVariableValueStorage(compositeValue);

			var arrayValue = value as IArrayParameterValue;
			if (arrayValue != null)
				return new ArrayVariableValueStorage(arrayValue);

			throw new NotSupportedException("Unsupported parameter value type");
		}

		private class PrimitiveVariableValueStorage : VariableValueStorage
		{
			private readonly object value;

			public PrimitiveVariableValueStorage(object value)
			{
				this.value = value;
			}

			public override object TryGetValue(VariableOccurence variableOccurence)
			{
				if (variableOccurence.Member != null)
					throw new InvalidOperationException(
						"Trying to get a primitive value for a variable container, not the variable itself");

				switch (variableOccurence.RequiredType)
				{
					case VariableType.Unknown:
					case VariableType.Composite:
					case VariableType.Array:
						throw new InvalidOperationException("Trying to get a primitive value for a variable of a wrong type");

					case VariableType.Boolean:
					case VariableType.Integer:
					case VariableType.String:
					case VariableType.Primitive:
						return value;

					default:
						throw new NotImplementedException("Unsupported variable type");
				}
			}
		}

		private class CompositeVariableValueStorage : VariableValueStorage
		{
			private readonly IDictionary<string, VariableValueStorage> fields;

			public CompositeVariableValueStorage(ICompositeParameterValue parameterValue)
			{
				if (parameterValue == null)
					throw new ArgumentNullException(nameof(parameterValue));

				fields = parameterValue
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

			public override object TryGetValue(VariableOccurence variableOccurence)
			{
				if (variableOccurence.Member != null && variableOccurence.RequiredType != VariableType.Composite)
					throw new InvalidOperationException("Trying to get the composite value for a variable of a wrong type");

				VariableValueStorage field;
				if (fields.TryGetValue(variableOccurence.Name, out field))
					return field.TryGetValue(variableOccurence.Member ?? variableOccurence);
				else
					return null;
			}
		}

		private class ArrayVariableValueStorage : VariableValueStorage
		{
			private readonly IEnumerable<VariableValueStorage> elements;

			public ArrayVariableValueStorage(IArrayParameterValue parameterValue)
			{
				if (parameterValue == null)
					throw new ArgumentNullException(nameof(parameterValue));

				elements = parameterValue
					.Values
					.Select(CreateStorageForValue)
					.ToList()
					.AsReadOnly();
			}

			public override object TryGetValue(VariableOccurence variableOccurence)
			{
				if (variableOccurence.RequiredType != VariableType.Array)
					throw new InvalidOperationException("Trying to get the array value for a variable of a wrong type");
				return elements;
			}
		}
	}
}
