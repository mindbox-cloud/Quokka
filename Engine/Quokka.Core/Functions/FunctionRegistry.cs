// // Copyright 2022 Mindbox Ltd
// //
// // Licensed under the Apache License, Version 2.0 (the "License");
// // you may not use this file except in compliance with the License.
// // You may obtain a copy of the License at
// //
// //     http://www.apache.org/licenses/LICENSE-2.0
// //
// // Unless required by applicable law or agreed to in writing, software
// // distributed under the License is distributed on an "AS IS" BASIS,
// // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// // See the License for the specific language governing permissions and
// // limitations under the License.

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
