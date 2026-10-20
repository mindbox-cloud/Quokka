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

using Mindbox.Quokka.Abstractions;

namespace Mindbox.Quokka
{
	internal class IfTemplateFunction : ScalarTemplateFunction<bool, string, string, string>
	{
		private static readonly BoolFunctionArgument conditionArgument = new("condition");
		private static readonly StringFunctionArgument trueValueArgument = new("trueValue");
		private static readonly StringFunctionArgument falseValueArgument = new("falseValue");

		public IfTemplateFunction()
			: base(
				  "if",
				  conditionArgument,
				  trueValueArgument,
				  falseValueArgument)
		{
		}

		public override string Invoke(RenderSettings settings, bool argument1, string argument2, string argument3)
		{
			return argument1 ? argument2 : argument3;
		}

		/// <summary>
		/// Evaluates the condition and then only the chosen value, so that the value that is not chosen
		/// can't fail the rendering (e.g. by using a null value the condition checks for).
		/// </summary>
		internal override VariableValueStorage InvokeWithDeferredArguments(
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
