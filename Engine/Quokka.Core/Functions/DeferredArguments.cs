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
	/// <summary>
	/// Arguments of a function call. An argument is evaluated only when <see cref="Evaluate"/> is called,
	/// so the function decides which of its arguments are evaluated. It is a ref struct, so the arguments
	/// can't be evaluated after the invocation of the function, where their errors couldn't be reported.
	/// </summary>
	internal readonly ref struct DeferredArguments
	{
		private readonly RenderContext renderContext;
		private readonly TemplateFunction function;
		private readonly IReadOnlyList<ArgumentValue> argumentValues;
		private readonly ref Exception argumentError;

		/// <param name="argumentError">Receives the last error of an argument expression,
		/// so that the caller can tell it from an error of the function itself.</param>
		public DeferredArguments(
			RenderContext renderContext,
			TemplateFunction function,
			IReadOnlyList<ArgumentValue> argumentValues,
			ref Exception argumentError)
		{
			this.renderContext = renderContext;
			this.function = function;
			this.argumentValues = argumentValues;
			this.argumentError = ref argumentError;
		}

		public int Count => argumentValues.Count;

		/// <summary>
		/// Evaluates the argument. Each call evaluates it again.
		/// </summary>
		public VariableValueStorage Evaluate(int argumentNumber)
		{
			var argumentValue = argumentValues[argumentNumber];
			var templateFunctionArgument = function.Arguments.GetArgument(argumentNumber);

			try
			{
				return argumentValue.GetValue(renderContext, templateFunctionArgument);
			}
			catch (Exception ex)
			{
				argumentError = ex;
				throw;
			}
		}
	}
}
