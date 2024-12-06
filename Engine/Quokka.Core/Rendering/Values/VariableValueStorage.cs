// // Copyright 2022 Mindbox Ltd
// //
// // Licensed under the Apache License, Version 2.0 (the "License");
// // you may not use this file except in compliance with the License.
// // You may obtain a copy of the License at
// //
// //     http://www.apache.org/licenses/LICENSE-2.0
// //
// // Unless required by applicable law or agreed to in writing, software
// // distributed under the License is distributed on an "AS IS" BASIS,
// // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// // See the License for the specific language governing permissions and
// // limitations under the License.

using System;
using System.Collections.Generic;

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
			ArgumentNullException.ThrowIfNull(value);

			return value switch
			{
				IPrimitiveModelValue primitiveValue => new PrimitiveVariableValueStorage(primitiveValue),
				IArrayModelValue arrayValue => new ArrayVariableValueStorage(arrayValue),
				ICompositeModelValue compositeValue => new CompositeVariableValueStorage(compositeValue),
				_ => throw new NotSupportedException($"Unsupported parameter value type {value.GetType().Name}")
			};
		}
	}
}
