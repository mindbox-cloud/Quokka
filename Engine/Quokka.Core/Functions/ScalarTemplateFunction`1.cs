using System;
using System.Collections.Generic;

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

		public abstract TResult Invoke(TArgument value);

		internal override object GetScalarInvocationResult(IList<VariableValueStorage> argumentsValues)
		{
			if (argumentsValues.Count != 1)
				throw new InvalidOperationException($"Function that expects 1 argument was passed {argumentsValues.Count}");

			return Invoke(argument.ConvertValue(argumentsValues[0]));
		}
	}
}
