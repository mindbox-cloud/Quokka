using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Quokka
{
	internal class FunctionRegistry
	{
		private readonly IReadOnlyDictionary<string, TemplateFunction> functions; 

		public FunctionRegistry(IEnumerable<TemplateFunction> functions)
		{
			this.functions = new ReadOnlyDictionary<string, TemplateFunction>(
				functions.ToDictionary(
					function => function.Name,
					StringComparer.InvariantCultureIgnoreCase));
		}

		public TemplateFunction TryGetFunction(FunctionCall functionCall)
		{
			TemplateFunction result;
			functions.TryGetValue(functionCall.FunctionName, out result);
			return result;
		}
	}
}
