using System;
using System.Collections.Generic;

namespace Quokka
{
	public abstract class TemplateFunction<TResult, TArgument1, TArgument2> : TemplateFunction
	{
		protected TemplateFunction(string name)
			: base(name, typeof(TResult), typeof(TArgument1), typeof(TArgument2))
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
