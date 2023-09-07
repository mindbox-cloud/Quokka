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
	public abstract class ScalarTemplateFunction<TArgument1, TArgument2, TArgument3, TResult> : ScalarTemplateFunction
	{
		private readonly ScalarArgument<TArgument1> argument1;
		private readonly ScalarArgument<TArgument2> argument2;
		private readonly ScalarArgument<TArgument3> argument3;

		protected ScalarTemplateFunction(
			string name,
			ScalarArgument<TArgument1> argument1,
			ScalarArgument<TArgument2> argument2,
			ScalarArgument<TArgument3> argument3)
				: base(name, typeof(TResult), argument1, argument2, argument3)
		{
			this.argument1 = argument1;
			this.argument2 = argument2;
			this.argument3 = argument3;
		}

		public abstract TResult Invoke(RenderSettings settings, TArgument1 value1, TArgument2 value2, TArgument3 value3);

		internal override object GetScalarInvocationResult(
			RenderContext renderContext,
			IList<VariableValueStorage> argumentsValues)
		{
			if (argumentsValues.Count != 3)
				throw new InvalidOperationException($"Function that expects 3 arguments was passed {argumentsValues.Count}");

			return Invoke(
				renderContext.Settings,
				argument1.ConvertValue(argumentsValues[0]),
				argument2.ConvertValue(argumentsValues[1]),
				argument3.ConvertValue(argumentsValues[2]));
		}
	}
}
