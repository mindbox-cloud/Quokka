﻿using System;
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
			if (variableOccurence == null)
				throw new ArgumentNullException(nameof(variableOccurence));

			if (valueStorage.ContainsValueForVariable(variableOccurence))
			{
				var variableValue = valueStorage.GetPrimitiveValue(variableOccurence);
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
				return valueStorage.GetElements(variableOccurence);
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

			return valueStorage.CheckIfValueIsNull(variableOccurence);
		}
	}
}
