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
		
		internal override VariableValueStorage Invoke(RenderContext renderContext, IList<VariableValueStorage> argumentsValues)
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