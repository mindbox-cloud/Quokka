using System;
using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class RandomTextFunction : VariadicScalarTemplateFunction<string>
	{
		private readonly Random random = new Random();
		
		public RandomTextFunction() 
			: base(
				"randomText",
				typeof(string),
				new VariadicArgument<string>(new StringFunctionArgument("inputString")))
		{
		}

		internal override object GetScalarInvocationResult(IList<VariableValueStorage> argumentsValues)
		{
			if (!argumentsValues.Any())
				return string.Empty;
			var next = random.Next(0, argumentsValues.Count - 1);
			return VariadicArgument.ConvertValue(argumentsValues[next]);
		}
	}
}