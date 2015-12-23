using System;
using System.Collections.Generic;

namespace Quokka
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
			if (valueStorage == null)
				throw new ArgumentNullException(nameof(valueStorage));

			this.valueStorage = valueStorage;
			this.parentScope = parentScope;
		}

		public RuntimeVariableScope CreateChildScope(CompositeVariableValueStorage valueStorage)
		{
			return new RuntimeVariableScope(valueStorage, this);
		}

		public VariableValueStorage GetValueStorageForVariable(VariableOccurence variableOccurence)
		{
			if (valueStorage.ContainsValueForVariable(variableOccurence))
			{
				var variableValueStorage = valueStorage.GetValueStorageForVariable(variableOccurence);
				if (variableValueStorage == null)
					throw new UnrenderableTemplateModelException(
						$"An attempt to use the value for variable {variableOccurence.GetLeafMemberFullName()} which happens to be null");

				return variableValueStorage;
			}
			else
			{
				if (parentScope == null)
					throw new InvalidOperationException($"Value for variable {variableOccurence.GetLeafMemberFullName()} not found");
				else
					return parentScope.GetValueStorageForVariable(variableOccurence);
			}
		}

		public object GetVariableValue(VariableOccurence variableOccurence)
		{
			if (variableOccurence == null)
				throw new ArgumentNullException(nameof(variableOccurence));

			if (valueStorage.ContainsValueForVariable(variableOccurence))
			{
				var variableValue = valueStorage.GetLeafMemberValueStorage(variableOccurence).GetPrimitiveValue();
				if (variableValue == null)
					throw new UnrenderableTemplateModelException(
						$"An attempt to use the value for variable {variableOccurence.GetLeafMemberFullName()} which happens to be null");

				return variableValue;
			}
			else
			{
				if (parentScope == null)
					throw new InvalidOperationException($"Value for variable {variableOccurence.GetLeafMemberFullName()} not found");
				else
					return parentScope.GetVariableValue(variableOccurence);
			}
		}

		public IEnumerable<VariableValueStorage> GetVariableValueCollection(VariableOccurence variableOccurence)
		{
			if (variableOccurence == null)
				throw new ArgumentNullException(nameof(variableOccurence));

			if (valueStorage.ContainsValueForVariable(variableOccurence))
			{
				return valueStorage.GetLeafMemberValueStorage(variableOccurence).GetElements();
			}
			else
			{
				if (parentScope == null)
					throw new InvalidOperationException($"Value for variable {variableOccurence.GetLeafMemberFullName()} not found");
				else
					return parentScope.GetVariableValueCollection(variableOccurence);
			}
		}


		public bool CheckIfVariableIsNull(VariableOccurence variableOccurence)
		{
			if (variableOccurence == null)
				throw new ArgumentNullException(nameof(variableOccurence));

			return valueStorage.GetLeafMemberValueStorage(variableOccurence).CheckIfValueIsNull();
		}
	}
}
