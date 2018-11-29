﻿using System;
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
