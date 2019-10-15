using System;
using System.Collections.Generic;

namespace Mindbox.Quokka
{
	internal class ChooseRandomTextFunction : VariadicScalarTemplateFunction<string>
	{
		private readonly Random random = new Random();
		
		public ChooseRandomTextFunction() 
			: base(
				"chooseRandomText",
				typeof(string),
				new VariadicArgument<string>(new StringFunctionArgument("inputString"), 1))
		{
		}

		internal override object GetScalarInvocationResult(
			RenderContext renderContext,
			IList<VariableValueStorage> argumentsValues)
		{
			var next = random.Next(0, argumentsValues.Count);
			return VariadicArgument.ConvertValue(argumentsValues[next]);
		}
	}
}