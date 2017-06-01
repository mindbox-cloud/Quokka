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
			if (valueStorage.ContainsValueForVariable(variableName))
				return valueStorage.GetFieldValueStorage(variableName);

			if (parentScope == null)
				throw new InvalidOperationException($"Value for variable {variableName} not found");

			return parentScope.TryGetValueStorageForVariable(variableName);
		}
	}
}
