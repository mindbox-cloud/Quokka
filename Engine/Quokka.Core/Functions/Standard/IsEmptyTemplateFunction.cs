using System;
using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	internal class IsEmptyTemplateFunction : TemplateFunction
	{
		public IsEmptyTemplateFunction()
			: base("isEmpty",
					new PrimitiveModelDefinition(TypeDefinition.Boolean),
					new AnyTemplateFunctionArgument("value"))
		{
		}
		
		internal override VariableValueStorage Invoke(IList<VariableValueStorage> argumentsValues)
		{
			if (argumentsValues.Count != 1)
				throw new InvalidOperationException($"Function that expects 1 argument was passed {argumentsValues.Count}");

			var isEmpty = false;
			var storage = argumentsValues.Single();
			if (storage.CheckIfValueIsNull())
			{
				isEmpty = true;
			}
			else if (storage is PrimitiveVariableValueStorage primitiveVariableValueStorage)
			{
				var value = primitiveVariableValueStorage.GetPrimitiveValue();
				if (value is string stringValue)
					isEmpty = string.IsNullOrWhiteSpace(stringValue);
			}
			return new PrimitiveVariableValueStorage(isEmpty);
		}
	}
}