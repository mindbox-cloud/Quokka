using System;
using System.Collections.Generic;

namespace Quokka
{
	internal class RuntimeVariableScope
	{
		private readonly RuntimeVariableScope parentScope;
		private readonly VariableValueStorage valueStorage;

		public RuntimeVariableScope(VariableValueStorage valueStorage)
			:this(valueStorage, null)
		{
		}

		private RuntimeVariableScope(VariableValueStorage valueStorage, RuntimeVariableScope parentScope)
		{
			if (valueStorage == null)
				throw new ArgumentNullException(nameof(valueStorage));

			this.valueStorage = valueStorage;
			this.parentScope = parentScope;
		}

		public RuntimeVariableScope CreateChildScope(VariableValueStorage valueStorage)
		{
			return new RuntimeVariableScope(valueStorage, this);
		}

		public object GetVariableValue(VariableOccurence variableOccurence)
		{
			if (valueStorage.ContainsValueForVariable(variableOccurence))
			{
				return valueStorage.GetPrimitiveValue(variableOccurence);
			}
			else
			{
				if (parentScope == null)
					throw new InvalidOperationException($"Value for variable {variableOccurence.Name} not found");
				else
					return parentScope.GetVariableValue(variableOccurence);
			}
		}

		public IEnumerable<VariableValueStorage> GetVariableValueCollection(VariableOccurence variableOccurence)
		{
			if (valueStorage.ContainsValueForVariable(variableOccurence))
			{
				return valueStorage.GetElements(variableOccurence);
			}
			else
			{
				if (parentScope == null)
					throw new InvalidOperationException($"Value for variable {variableOccurence.Name} not found");
				else
					return parentScope.GetVariableValueCollection(variableOccurence);
			}
		}
	}
}
