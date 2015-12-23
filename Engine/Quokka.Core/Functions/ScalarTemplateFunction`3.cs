using System;
using System.Collections.Generic;

namespace Quokka
{
	public abstract class ScalarTemplateFunction<TArgument1, TArgument2, TArgument3, TResult> : ScalarTemplateFunction
	{
		private readonly TemplateFunctionArgument<TArgument1> argument1;
		private readonly TemplateFunctionArgument<TArgument2> argument2;
		private readonly TemplateFunctionArgument<TArgument3> argument3;

		protected ScalarTemplateFunction(
			string name,
			TemplateFunctionArgument<TArgument1> argument1,
			TemplateFunctionArgument<TArgument2> argument2,
			TemplateFunctionArgument<TArgument3> argument3)
				: base(name, typeof(TResult), argument1, argument2, argument3)
		{
			this.argument1 = argument1;
			this.argument2 = argument2;
			this.argument3 = argument3;
		}

		public abstract TResult Invoke(TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		internal override object GetScalarInvocationResult(IList<VariableValueStorage> argumentsValues)
		{
			if (argumentsValues.Count != 3)
				throw new InvalidOperationException($"Function that expects 3 arguments was passed {argumentsValues.Count}");

			return Invoke(
				argument1.ConvertValue(argumentsValues[0]),
				argument2.ConvertValue(argumentsValues[1]),
				argument3.ConvertValue(argumentsValues[2]));
		}
	}
}
