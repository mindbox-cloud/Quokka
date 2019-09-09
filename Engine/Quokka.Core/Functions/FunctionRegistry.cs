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

		public TemplateFunction? TryGetFunction(string functionName, IReadOnlyList<ArgumentValue> arguments)
		{
			functions.TryGetValue(functionName, out var overloadedFunctions);
			return overloadedFunctions?.FirstOrDefault(function => function.Accepts(arguments));
		}

		public void PerformSemanticAnalysis(
			AnalysisContext context, 
			string functionName, 
			IReadOnlyList<ArgumentValue> arguments, 
			Location location)
		{
			functions.TryGetValue(functionName, out var overloadedFunctions);
			if (overloadedFunctions != null)
			{
				var suitableFunction = overloadedFunctions.FirstOrDefault(function => function.Accepts(arguments));
				if (suitableFunction == null)
				{
					context.ErrorListener.AddInvalidFunctionArgumentCountError(
						functionName,
						overloadedFunctions.Select(function => function.Arguments.Arguments.Count).ToArray(),
						arguments.Count,
						location);
				}
				else
				{
					suitableFunction.Arguments.PerformSemanticAnalysis(context, arguments, location);
				}
			}
			else
			{
				context.ErrorListener.AddUndefinedFunctionError(functionName, location);
			}
		}
	}
}
