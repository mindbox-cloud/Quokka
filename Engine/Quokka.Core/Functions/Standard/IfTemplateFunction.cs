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

namespace Mindbox.Quokka
{
	internal class IfTemplateFunction : TemplateFunction
	{
		private static readonly BoolFunctionArgument conditionArgument = new("condition");
		private static readonly StringFunctionArgument trueValueArgument = new("trueValue");
		private static readonly StringFunctionArgument falseValueArgument = new("falseValue");

		public IfTemplateFunction()
			: base(
				  "if",
				  new PrimitiveModelDefinition(TypeDefinition.String),
				  conditionArgument,
				  trueValueArgument,
				  falseValueArgument)
		{
		}

		/// <summary>
		/// Evaluates the condition and then only the chosen value. The value that is not chosen isn't evaluated,
		/// so a null value in it (e.g. the one the condition checks for) can't fail the rendering.
		/// Fields used in it are still required by the model.
		/// </summary>
		internal override VariableValueStorage Invoke(
			RenderContext renderContext,
			DeferredArguments deferredArguments)
		{
			if (deferredArguments.Count != 3)
				throw new InvalidOperationException($"Function that expects 3 arguments was passed {deferredArguments.Count}");

			var condition = conditionArgument.ConvertValue(deferredArguments.Evaluate(0));

			var chosenValue = condition
				? trueValueArgument.ConvertValue(deferredArguments.Evaluate(1))
				: falseValueArgument.ConvertValue(deferredArguments.Evaluate(2));

			return new PrimitiveVariableValueStorage(chosenValue);
		}
	}
}
