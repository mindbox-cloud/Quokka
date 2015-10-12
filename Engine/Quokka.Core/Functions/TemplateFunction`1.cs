using System;
using System.Collections.Generic;

namespace Quokka
{
	public abstract class TemplateFunction<TResult, TArgument1> : TemplateFunction
	{
		protected TemplateFunction(string name)
			: base(name, typeof(TResult), typeof(TArgument1))
		{
		}

		public abstract TResult Invoke(TArgument1 argument1);

		internal override object Invoke(IList<object> arguments)
		{
			if (arguments.Count != 1)
				throw new InvalidOperationException($"Function that expects 1 argument was passed {arguments.Count}");

			return Invoke((TArgument1)arguments[0]);
		}
	}
}
