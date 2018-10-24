using System;
using System.Collections.Generic;

namespace Mindbox.Quokka
{
	public abstract class ScalarTemplateFunction<TArgument1, TArgument2, TArgument3, TArgument4, TResult> : ScalarTemplateFunction
	{
		private readonly ScalarArgument<TArgument1> argument1;
		private readonly ScalarArgument<TArgument2> argument2;
		private readonly ScalarArgument<TArgument3> argument3;
		private readonly ScalarArgument<TArgument4> argument4;

		protected ScalarTemplateFunction(
			string name,
			ScalarArgument<TArgument1> argument1,
			ScalarArgument<TArgument2> argument2,
			ScalarArgument<TArgument3> argument3,
			ScalarArgument<TArgument4> argument4)
				: base(name, typeof(TResult), argument1, argument2, argument3, argument4)
		{
			this.argument1 = argument1;
			this.argument2 = argument2;
			this.argument3 = argument3;
			this.argument4 = argument4;
		}

		public abstract TResult Invoke(TArgument1 value1, TArgument2 value2, TArgument3 value3, TArgument4 value4);

		internal override object GetScalarInvocationResult(
			RenderContext renderContext,
			IList<VariableValueStorage> argumentsValues)
		{
			if (argumentsValues.Count != 4)
				throw new InvalidOperationException($"Function that expects 4 arguments was passed {argumentsValues.Count}");

			return Invoke(
				argument1.ConvertValue(argumentsValues[0]), 
				argument2.ConvertValue(argumentsValues[1]), 
				argument3.ConvertValue(argumentsValues[2]), 
				argument4.ConvertValue(argumentsValues[3]));
		}
	}
}
