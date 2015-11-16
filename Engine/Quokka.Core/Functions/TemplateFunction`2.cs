using System;
using System.Collections.Generic;

namespace Quokka
{
	public abstract class TemplateFunction<TArgument1, TArgument2, TResult> : TemplateFunction
	{
		private readonly TemplateFunctionArgument<TArgument1> argument1;
		private readonly TemplateFunctionArgument<TArgument2> argument2;

		protected TemplateFunction(
			string name,
			TemplateFunctionArgument<TArgument1> argument1,
			TemplateFunctionArgument<TArgument2> argument2)
				: base(name, typeof(TResult), argument1, argument2)
		{
			this.argument1 = argument1;
			this.argument2 = argument2;
		}

		public abstract TResult Invoke(TArgument1 argument1, TArgument2 argument2);

		internal override object Invoke(IList<object> argumentsValues)
		{
			if (argumentsValues.Count != 2)
				throw new InvalidOperationException($"Function that expects 2 arguments was passed {argumentsValues.Count}");

			return Invoke(
				argument1.ConvertValue(argumentsValues[0]), 
				argument2.ConvertValue(argumentsValues[1]));
		}
	}
}
