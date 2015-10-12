using System;
using System.Collections.Generic;

namespace Quokka
{
	public abstract class TemplateFunction<TArgument, TResult> : TemplateFunction
	{
		protected TemplateFunction(string name, TemplateFunctionArgument<TArgument> argument)
			: base(name, typeof(TResult), argument)
		{
		}

		public abstract TResult Invoke(TArgument argument);

		internal override object Invoke(IList<object> arguments)
		{
			if (arguments.Count != 1)
				throw new InvalidOperationException($"Function that expects 1 argument was passed {arguments.Count}");

			return Invoke((TArgument)arguments[0]);
		}
	}
}
