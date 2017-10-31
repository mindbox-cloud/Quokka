using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mindbox.Quokka
{
	internal class FunctionRegistry
	{
		private readonly IReadOnlyDictionary<string, TemplateFunction[]> functions; 

		public FunctionRegistry(IEnumerable<TemplateFunction> functions)
		{
			this.functions = new ReadOnlyDictionary<string, TemplateFunction[]>(
				functions
					.GroupBy(function => function.Name)
					.ToDictionary(
						grouping => grouping.Key,
						grouping => grouping.ToArray(),
					StringComparer.InvariantCultureIgnoreCase));
		}

		public TemplateFunction TryGetFunction(string functionName, IReadOnlyList<ArgumentValue> arguments)
		{
			functions.TryGetValue(functionName, out var overloadedFunctions);
			return overloadedFunctions?.FirstOrDefault(function => function.Accepts(arguments));
		}
	}
}
