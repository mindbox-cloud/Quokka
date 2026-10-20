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
	internal class IfTemplateFunction : TemplateFunction
	{
		private readonly BoolFunctionArgument conditionArgument;
		private readonly StringFunctionArgument trueValueArgument;
		private readonly StringFunctionArgument falseValueArgument;

		public IfTemplateFunction()
			: this(
				  new BoolFunctionArgument("condition"),
				  new StringFunctionArgument("trueValue"),
				  new StringFunctionArgument("falseValue"))
		{
		}

		private IfTemplateFunction(
			BoolFunctionArgument conditionArgument,
			StringFunctionArgument trueValueArgument,
			StringFunctionArgument falseValueArgument)
			: base(
				  "if",
				  new PrimitiveModelDefinition(TypeDefinition.String),
				  conditionArgument,
				  trueValueArgument,
				  falseValueArgument)
		{
			this.conditionArgument = conditionArgument;
			this.trueValueArgument = trueValueArgument;
			this.falseValueArgument = falseValueArgument;
		}

		/// <summary>
		/// Evaluates the condition and then only the chosen value, so that the value that is not chosen
		/// can't fail the rendering (e.g. by using a null value the condition checks for).
		/// </summary>
		internal override VariableValueStorage Invoke(
			RenderContext renderContext,
			IReadOnlyList<Func<VariableValueStorage>> argumentValueProviders)
		{
			var condition = conditionArgument.ConvertValue(argumentValueProviders[0]());

			var chosenValue = condition
				? trueValueArgument.ConvertValue(argumentValueProviders[1]())
				: falseValueArgument.ConvertValue(argumentValueProviders[2]());

			return new PrimitiveVariableValueStorage(chosenValue);
		}
	}
}
