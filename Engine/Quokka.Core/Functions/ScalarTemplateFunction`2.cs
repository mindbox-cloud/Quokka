using System;
using System.Collections.Generic;

namespace Mindbox.Quokka
{
	public abstract class ScalarTemplateFunction<TArgument1, TArgument2, TResult> : ScalarTemplateFunction
	{
		private readonly ScalarArgument<TArgument1> argument1;
		private readonly ScalarArgument<TArgument2> argument2;

		protected ScalarTemplateFunction(
			string name,
			ScalarArgument<TArgument1> argument1,
			ScalarArgument<TArgument2> argument2)
				: base(name, typeof(TResult), argument1, argument2)
		{
			this.argument1 = argument1;
			this.argument2 = argument2;
		}

		public abstract TResult Invoke(TArgument1 value1, TArgument2 value2);

		internal override object GetScalarInvocationResult(IList<VariableValueStorage> argumentsValues)
		{
			if (argumentsValues.Count != 2)
				throw new InvalidOperationException($"Function that expects 2 arguments was passed {argumentsValues.Count}");

			return Invoke(
				argument1.ConvertValue(argumentsValues[0]), 
				argument2.ConvertValue(argumentsValues[1]));
		}
	}
}
