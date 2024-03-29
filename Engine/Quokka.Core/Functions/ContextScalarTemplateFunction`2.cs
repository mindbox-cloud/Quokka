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

namespace Mindbox.Quokka
{
	public abstract class ContextScalarTemplateFunction<TContext, TArgument1, TArgument2, TResult> : ContextScalarTemplateFunction<TContext>
		where TContext : class
	{
		private readonly ScalarArgument<TArgument1> argument1;
		private readonly ScalarArgument<TArgument2> argument2;

		protected ContextScalarTemplateFunction(
			string name,
			ScalarArgument<TArgument1> argument1,
			ScalarArgument<TArgument2> argument2)
			: base(name, typeof(TResult), argument1, argument2)
		{
			this.argument1 = argument1;
			this.argument2 = argument2;
		}

		protected abstract TResult Invoke(TContext context, TArgument1 argumentValue1, TArgument2 argumentValue2);

		internal sealed override object GetContextScalarInvocationResult(TContext context, IList<VariableValueStorage> argumentsValues)
		{
			if (argumentsValues.Count != 2)
				throw new InvalidOperationException($"Function that expects 2 arguments was passed {argumentsValues.Count}");

			return Invoke(
				context,
				argument1.ConvertValue(argumentsValues[0]),
				argument2.ConvertValue(argumentsValues[1]));
		}
	}
}
