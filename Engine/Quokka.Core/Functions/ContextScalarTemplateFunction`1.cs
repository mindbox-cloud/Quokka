using System;
using System.Collections.Generic;

namespace Mindbox.Quokka
{
	public abstract class ContextScalarTemplateFunction<TContext, TArgument, TResult> : ContextScalarTemplateFunction<TContext>
		where TContext : class
	{
		private readonly ScalarArgument<TArgument> argument;

		protected ContextScalarTemplateFunction(string name, ScalarArgument<TArgument> argument)
			: base(name, typeof(TResult), argument)
		{
			this.argument = argument;
		}

		public abstract TResult Invoke(TContext context, TArgument value);

		internal override object GetContextScalarInvocationResult(TContext context, IList<VariableValueStorage> argumentsValues)
		{
			if (argumentsValues.Count != 1)
				throw new InvalidOperationException($"Function that expects 1 argument was passed {argumentsValues.Count}");

			return Invoke(context, argument.ConvertValue(argumentsValues[0]));
		}
	}
}
