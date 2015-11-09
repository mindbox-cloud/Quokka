using System;
using System.Collections.Generic;

namespace Quokka
{
	public abstract class TemplateFunction<TArgument, TResult> : TemplateFunction
	{
		private readonly TemplateFunctionArgument<TArgument> argument;

		protected TemplateFunction(string name, TemplateFunctionArgument<TArgument> argument)
			: base(name, typeof(TResult), argument)
		{
			this.argument = argument;
		}

		public abstract TResult Invoke(TArgument argument);

		internal override object Invoke(IList<object> argumentsValues)
		{
			if (argumentsValues.Count != 1)
				throw new InvalidOperationException($"Function that expects 1 argument was passed {argumentsValues.Count}");

			return Invoke(argument.ConvertValue(argumentsValues[0]));
		}
	}
}
