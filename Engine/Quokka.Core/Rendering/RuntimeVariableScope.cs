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
	internal class RuntimeVariableScope
	{
		private readonly RuntimeVariableScope parentScope;
		private readonly CompositeVariableValueStorage valueStorage;

		public RuntimeVariableScope(CompositeVariableValueStorage valueStorage)
			:this(valueStorage, null)
		{
		}

		private RuntimeVariableScope(CompositeVariableValueStorage valueStorage, RuntimeVariableScope parentScope)
		{
			this.valueStorage = valueStorage;
			this.parentScope = parentScope;
		}

		public RuntimeVariableScope CreateChildScope(CompositeVariableValueStorage childScopeValueStorage)
		{
			return new RuntimeVariableScope(childScopeValueStorage, this);
		}
		
		public VariableValueStorage TryGetValueStorageForVariable(string variableName)
		{
			return valueStorage.ContainsValueForVariable(variableName) 
						? valueStorage.GetFieldValueStorage(variableName) 
						: parentScope?.TryGetValueStorageForVariable(variableName);
		}

		public void TrySetValueStorageForVariable(string variableName, VariableValueStorage value)
		{
			var scope = TryGetScopeForVariable(variableName) ?? this;

			scope.valueStorage.SetFieldValueStorage(variableName, value);
		}

		private RuntimeVariableScope TryGetScopeForVariable(string variableName)
		{
			if (valueStorage.ContainsValueForVariable(variableName))
				return this;

			return parentScope?.TryGetScopeForVariable(variableName);
		}
	}
}
