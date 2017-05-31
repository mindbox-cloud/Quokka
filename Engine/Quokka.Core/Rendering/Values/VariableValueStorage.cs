using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Mindbox.Quokka
{
	internal abstract class VariableValueStorage
	{
		public abstract IModelValue ModelValue { get; }

		public virtual object GetPrimitiveValue()
		{
			throw new InvalidOperationException("This storage can't provide values of this type");
		}

		public virtual bool CheckIfValueIsNull()
		{
			return false;
		}

		public virtual IEnumerable<VariableValueStorage> GetElements()
		{
			throw new InvalidOperationException("This storage can't provide values of this type");
		}

		public virtual VariableValueStorage GetFieldValueStorage(string memberName)
		{
			throw new InvalidOperationException("This storage type doesn't contain members");
		}
		public virtual VariableValueStorage GetMethodCallResultValueStorage(MethodCall methodCall)
		{
			throw new InvalidOperationException("This storage type doesn't contain members");
		}

		protected static VariableValueStorage CreateStorageForValue(IModelValue value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			switch (value)
			{
				case IPrimitiveModelValue primitiveValue:
					return new PrimitiveVariableValueStorage(primitiveValue);
				case IArrayModelValue arrayValue:
					return new ArrayVariableValueStorage(arrayValue);
				case ICompositeModelValue compositeValue:
					return new CompositeVariableValueStorage(compositeValue);
				default:
					throw new NotSupportedException($"Unsupported parameter value type {value.GetType().Name}");
			}
		}
	}
}
