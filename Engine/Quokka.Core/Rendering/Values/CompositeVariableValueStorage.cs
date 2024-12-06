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
using System.Collections.ObjectModel;
using System.Linq;

namespace Mindbox.Quokka
{
	internal class CompositeVariableValueStorage : VariableValueStorage
	{
		private readonly IDictionary<string, VariableValueStorage> fields;
		private readonly IDictionary<MethodCall, VariableValueStorage> methods;

		public override IModelValue ModelValue { get; }

		public CompositeVariableValueStorage(ICompositeModelValue modelValue)
		{
			ArgumentNullException.ThrowIfNull(modelValue);

			ModelValue = modelValue;

			fields = modelValue
				.Fields
				.ToDictionary(
					field => field.Name.Trim(),
					field => field.Value != null ? CreateStorageForValue(field.Value) : null,
					StringComparer.InvariantCultureIgnoreCase);

			methods = modelValue
				.Methods
				.ToDictionary(
					method => new MethodCall(method.Name, method.Arguments), 
					method => method.Value != null ? CreateStorageForValue(method.Value) : null);
		}

		public CompositeVariableValueStorage(string fieldName, VariableValueStorage fieldValueStorage)
		{
			ArgumentNullException.ThrowIfNull(fieldName);
			ArgumentNullException.ThrowIfNull(fieldValueStorage);

			fields = new Dictionary<string, VariableValueStorage>(StringComparer.InvariantCultureIgnoreCase)
			{
				{ fieldName, fieldValueStorage }
			};
		}

		public void SetFieldValueStorage(string variableName, VariableValueStorage value)
		{
			fields[variableName] = value;
		}

		public bool ContainsValueForVariable(string variableName)
		{
			return fields.ContainsKey(variableName);
		}

		public override VariableValueStorage GetFieldValueStorage(string memberName)
		{
			if (fields.TryGetValue(memberName, out VariableValueStorage fieldValue))
				return fieldValue;
			else
				throw new InvalidOperationException($"Member {memberName} not found");
		}

		public override VariableValueStorage GetMethodCallResultValueStorage(MethodCall methodCall)
		{
			if (methods.TryGetValue(methodCall, out VariableValueStorage methodValue))
				return methodValue;
			else
				throw new InvalidOperationException($"Method call result for {methodCall.Name} not found");
		}
	}
}