using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mindbox.Quokka
{
	internal class CompositeVariableValueStorage : VariableValueStorage
	{
		private readonly IDictionary<string, VariableValueStorage?> fields;
		private readonly IDictionary<MethodCall, VariableValueStorage?> methods;

		public override IModelValue ModelValue { get; }

		public CompositeVariableValueStorage(ICompositeModelValue modelValue)
		{
			if (modelValue == null)
				throw new ArgumentNullException(nameof(modelValue));

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

		public void SetFieldValueStorage(string variableName, VariableValueStorage value)
		{
			fields[variableName] = value;
		}

		public bool ContainsValueForVariable(string variableName)
		{
			return fields.ContainsKey(variableName);
		}

		public override VariableValueStorage? GetFieldValueStorage(string memberName)
		{
			if (fields.TryGetValue(memberName, out VariableValueStorage? fieldValue))
				return fieldValue;
			else
				throw new InvalidOperationException($"Member {memberName} not found");
		}

		public override VariableValueStorage? GetMethodCallResultValueStorage(MethodCall methodCall)
		{
			if (methods.TryGetValue(methodCall, out VariableValueStorage? methodValue))
				return methodValue;
			else
				throw new InvalidOperationException($"Method call result for {methodCall.Name} not found");
		}
	}
}