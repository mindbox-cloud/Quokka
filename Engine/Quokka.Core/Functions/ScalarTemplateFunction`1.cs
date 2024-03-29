﻿// // Copyright 2022 Mindbox Ltd
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
	public abstract class ScalarTemplateFunction<TArgument, TResult> : ScalarTemplateFunction
	{
		private readonly ScalarArgument<TArgument> argument;

		protected ScalarTemplateFunction(string name, ScalarArgument<TArgument> argument)
			: base(name, typeof(TResult), argument)
		{
			this.argument = argument;
		}

		public abstract TResult Invoke(RenderSettings settings, TArgument value);

		internal override object GetScalarInvocationResult(
			RenderContext renderContext,
			IList<VariableValueStorage> argumentsValues)
		{
			if (argumentsValues.Count != 1)
				throw new InvalidOperationException($"Function that expects 1 argument was passed {argumentsValues.Count}");

			return Invoke(renderContext.Settings, argument.ConvertValue(argumentsValues[0]));
		}
	}
}
