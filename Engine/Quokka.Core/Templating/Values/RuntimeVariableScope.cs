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
			if (valueStorage == null)
				throw new ArgumentNullException(nameof(valueStorage));

			this.valueStorage = valueStorage;
			this.parentScope = parentScope;
		}

		public RuntimeVariableScope CreateChildScope(CompositeVariableValueStorage valueStorage)
		{
			return new RuntimeVariableScope(valueStorage, this);
		}
		
		public VariableValueStorage TryGetValueStorageForVariable(string variableName, bool allowNullVariableValues = true)
		{
			if (valueStorage.ContainsValueForVariable(variableName))
			{
				try
				{
					var variableValueStorage = valueStorage.GetMemberValueStorage(variableName);
					if (variableValueStorage == null || (!allowNullVariableValues && variableValueStorage.CheckIfValueIsNull()))
						throw PrepareUnrenderableVariableAccessException(variableName);

					return variableValueStorage;
				}
				catch (ValueStorageAccessException)
				{
					throw PrepareUnrenderableVariableAccessException(variableName);
				}
			}
			if (parentScope == null)
				throw new InvalidOperationException($"Value for variable {variableName} not found");

			return parentScope.TryGetValueStorageForVariable(variableName, allowNullVariableValues);
		}
		
		private UnrenderableTemplateModelException PrepareUnrenderableVariableAccessException(string accessedMember)
		{
			return new UnrenderableTemplateModelException(
				$"An attempt to use the value for variable {accessedMember} which happens to be null");
		}
	}
}
