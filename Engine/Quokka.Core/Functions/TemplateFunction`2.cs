using System;
using System.Collections.Generic;

namespace Quokka
{
	public abstract class TemplateFunction<TArgument1, TArgument2, TResult> : TemplateFunction
	{
		protected TemplateFunction(
			string name,
			TemplateFunctionArgument<TArgument1> argument1,
			TemplateFunctionArgument<TArgument2> argument2)
				: base(name, typeof(TResult), argument1, argument2)
		{
		}

		public abstract TResult Invoke(TArgument1 argument1, TArgument2 argument2);

		internal override object Invoke(IList<object> arguments)
		{
			if (arguments.Count != 2)
				throw new InvalidOperationException($"Function that expects 2 arguments was passed {arguments.Count}");

			return Invoke((TArgument1)arguments[0], (TArgument2)arguments[1]);
		}
	}
}
