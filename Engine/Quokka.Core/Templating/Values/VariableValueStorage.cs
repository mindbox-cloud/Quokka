using System;
using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal abstract class VariableValueStorage
	{
		public abstract object GetValue(VariableOccurence variableOccurence);

		public static VariableValueStorage CreateStorageFromValue(IParameterValue value)
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

			public override object GetValue(VariableOccurence variableOccurence)
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
						field => field.Name,
						field => CreateStorageFromValue(field.Value),
						StringComparer.InvariantCultureIgnoreCase);
			}

			public override object GetValue(VariableOccurence variableOccurence)
			{
				var field = fields[variableOccurence.Name];
				return field.GetValue(variableOccurence.Member ?? variableOccurence);
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
					.Select(CreateStorageFromValue)
					.ToList()
					.AsReadOnly();
			}

			public override object GetValue(VariableOccurence variableOccurence)
			{
				return elements;
			}
		}
	}
}
