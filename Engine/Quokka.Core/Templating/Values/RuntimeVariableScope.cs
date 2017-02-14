using System;
using System.Collections.Generic;

using Quokka.Templating;

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

		public VariableValueStorage GetValueStorageForVariable(VariableOccurence variableOccurence, bool allowNullVariableValues = true)
		{
			if (valueStorage.ContainsValueForVariable(variableOccurence))
			{
				try
				{
					var variableValueStorage = valueStorage.GetLeafMemberValueStorage(variableOccurence);
					if (variableValueStorage == null || (!allowNullVariableValues && variableValueStorage.CheckIfValueIsNull()))
						throw PrepareUnrenderableVariableAccessException(variableOccurence.GetLeafMemberFullName());

					return variableValueStorage;
				}
				catch (ValueStorageAccessException ex)
				{
					throw PrepareUnrenderableVariableAccessException(variableOccurence.GetMemberFullName(ex.Member));	
				}
			}
			if (parentScope == null)
				throw new InvalidOperationException($"Value for variable {variableOccurence.GetLeafMemberFullName()} not found");
			return parentScope.GetValueStorageForVariable(variableOccurence);
		}

		public object GetVariableValue(VariableOccurence variableOccurence)
		{
			var storage = GetValueStorageForVariable(variableOccurence, false);
			var value = storage.GetPrimitiveValue();
			if (value == null)
				throw PrepareUnrenderableVariableAccessException(variableOccurence.GetLeafMemberFullName());

			return value;
		}

		public IEnumerable<VariableValueStorage> GetVariableValueCollection(VariableOccurence variableOccurence)
		{
			if (variableOccurence == null)
				throw new ArgumentNullException(nameof(variableOccurence));

			if (valueStorage.ContainsValueForVariable(variableOccurence))
			{
				return valueStorage.GetLeafMemberValueStorage(variableOccurence).GetElements();
			}
			if (parentScope == null)
				throw new InvalidOperationException($"Value for variable {variableOccurence.GetLeafMemberFullName()} not found");
			return parentScope.GetVariableValueCollection(variableOccurence);
		}


		public bool CheckIfVariableIsNull(VariableOccurence variableOccurence)
		{
			if (variableOccurence == null)
				throw new ArgumentNullException(nameof(variableOccurence));

			if (valueStorage.ContainsValueForVariable(variableOccurence))
			{
				try
				{
					var leafValueStorage = valueStorage.GetLeafMemberValueStorage(variableOccurence);
					return leafValueStorage == null || leafValueStorage.CheckIfValueIsNull();
				}
				catch (ValueStorageAccessException)
				{
					return true;
				}
			}
			if (parentScope == null)
				throw new InvalidOperationException($"Value for variable {variableOccurence.GetLeafMemberFullName()} not found");

			return parentScope.CheckIfVariableIsNull(variableOccurence);

		}

		private UnrenderableTemplateModelException PrepareUnrenderableVariableAccessException(string accessedMember)
		{
			return new UnrenderableTemplateModelException(
				$"An attempt to use the value for variable {accessedMember} which happens to be null");
		}
	}
}
