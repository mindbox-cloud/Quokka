using System;
using System.Collections.Generic;

namespace Quokka
{
	public abstract class TemplateFunction<TArgument1, TArgument2, TArgument3, TResult> : TemplateFunction
	{
		protected TemplateFunction(
			string name,
			TemplateFunctionArgument<TArgument1> argument1,
			TemplateFunctionArgument<TArgument2> argument2,
			TemplateFunctionArgument<TArgument3> argument3)
				: base(name, typeof(TResult), argument1, argument2, argument3)
		{
		}

		public abstract TResult Invoke(TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		internal override object Invoke(IList<object> arguments)
		{
			if (arguments.Count != 3)
				throw new InvalidOperationException($"Function that expects 3 arguments was passed {arguments.Count}");

			return Invoke((TArgument1)arguments[0], (TArgument2)arguments[1], (TArgument3)arguments[2]);
		}
	}
}
