using System;
using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal abstract class VariableValueStorage
	{
		public virtual TValue GetPrimitiveValue<TValue>(VariableOccurence variableOccurence)
		{
			throw new InvalidOperationException("This storage can't provide values of this type");
		}

		public virtual IEnumerable<VariableValueStorage> GetElements(VariableOccurence variableOccurence)
		{
			throw new InvalidOperationException("This storage can't provide values of this type");
		}

		public virtual bool ContainsValueForVariable(VariableOccurence variableOccurence)
		{
			return true;
		}

		public static VariableValueStorage CreateCompositeStorage(string fieldName, VariableValueStorage fieldValueStorage)
		{
			return new CompositeVariableValueStorage(fieldName, fieldValueStorage);
		}

		public static VariableValueStorage CreateStorageForValue(IModelValue value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			var primitiveValue = value as IPrimitiveModelValue;
			if (primitiveValue != null)
				return new PrimitiveVariableValueStorage(primitiveValue);

			var compositeValue = value as ICompositeModelValue;
			if (compositeValue != null)
				return new CompositeVariableValueStorage(compositeValue);

			var arrayValue = value as IArrayModelValue;
			if (arrayValue != null)
				return new ArrayVariableValueStorage(arrayValue);

			throw new NotSupportedException("Unsupported parameter value type");
		}

		private class PrimitiveVariableValueStorage : VariableValueStorage
		{
			private readonly IPrimitiveModelValue primitiveModel;

			public PrimitiveVariableValueStorage(IPrimitiveModelValue primitiveModel)
			{
				this.primitiveModel = primitiveModel;
			}

			public override TValue GetPrimitiveValue<TValue>(VariableOccurence variableOccurence)
			{
				if (variableOccurence.Member != null)
					throw new InvalidOperationException(
						"Trying to get a primitive value for a variable container, not the variable itself");

				if (variableOccurence.RequiredType.IsCompatibleWithRequired(TypeDefinition.Primitive))
				{
					TValue result;
					if (!primitiveModel.TryGetValue<TValue>(out result))
						throw new InvalidOperationException("Could not obtain primtive value from the model");
					return result;
				}
				else
				{
					throw new NotImplementedException("Unsupported variable type");
				}
			}
		}

		private class CompositeVariableValueStorage : VariableValueStorage
		{
			private readonly IDictionary<string, VariableValueStorage> fields;

			public CompositeVariableValueStorage(ICompositeModelValue modelValue)
			{
				if (modelValue == null)
					throw new ArgumentNullException(nameof(modelValue));

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

			public override bool ContainsValueForVariable(VariableOccurence variableOccurence)
			{
				return fields.ContainsKey(variableOccurence.Name);
			}

			public override TValue GetPrimitiveValue<TValue>(VariableOccurence variableOccurence)
			{
				VariableValueStorage field;
				if (fields.TryGetValue(variableOccurence.Name, out field))
					return field.GetPrimitiveValue<TValue>(variableOccurence.Member ?? variableOccurence);
				else
					throw new InvalidOperationException($"Field {variableOccurence.Name} not found");
			}

			public override IEnumerable<VariableValueStorage> GetElements(VariableOccurence variableOccurence)
			{
				VariableValueStorage field;
				if (fields.TryGetValue(variableOccurence.Name, out field))
					return field.GetElements(variableOccurence.Member ?? variableOccurence);
				else
					throw new InvalidOperationException($"Field {variableOccurence.Name} not found");
			}
		}

		private class ArrayVariableValueStorage : VariableValueStorage
		{
			private readonly IEnumerable<VariableValueStorage> elements;

			public ArrayVariableValueStorage(IArrayModelValue modelValue)
			{
				if (modelValue == null)
					throw new ArgumentNullException(nameof(modelValue));

				elements = modelValue
					.Values
					.Select(CreateStorageForValue)
					.ToList()
					.AsReadOnly();
			}

			public override IEnumerable<VariableValueStorage> GetElements(VariableOccurence variableOccurence)
			{
				if (variableOccurence.RequiredType != TypeDefinition.Array)
					throw new InvalidOperationException("Trying to get the array value for a variable of a wrong type");
				return elements;
			}
		}
	}
}
