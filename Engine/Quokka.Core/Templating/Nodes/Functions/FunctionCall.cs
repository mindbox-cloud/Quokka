using System;
using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class FunctionCall
	{
		public string FunctionName { get; }

		public Location Location { get; }

		private readonly IReadOnlyList<IFunctionArgument> arguments; 

		public FunctionCall(string functionName, Location location, IEnumerable<IFunctionArgument> arguments)
		{
			FunctionName = functionName;
			Location = location;
			this.arguments = arguments.ToList().AsReadOnly();
		}

		public void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			var function = TryGetFunctionForSemanticAnalysis(context);

			function?.Arguments.CompileVariableDefinitions(context, arguments, Location);
		}
		
		public void MapArgumentVariableDefinitionsToResult(
			SemanticAnalysisContext context,
			VariableDefinition resultDefinition)
		{
			var function = TryGetFunctionForSemanticAnalysis(context);

			// We only do this if the template is semantically valid. If not, semantic errors are already handled
			// earlier in the workflow.
			function?.Arguments.MapArgumentVariableDefinitionsToResult(context, arguments, resultDefinition);
		}

		public VariableValueStorage GetInvocationResult(RenderContext renderContext)
		{
			var function = renderContext.Functions.TryGetFunction(FunctionName);
			if (function == null)
				throw new InvalidOperationException($"Function {FunctionName} not found");

			return function.Invoke(arguments.Select(arg => arg.GetValue(renderContext)).ToList());
		}

		public TemplateFunction TryGetFunctionForSemanticAnalysis(SemanticAnalysisContext context)
		{
			var function = context.Functions.TryGetFunction(FunctionName);
			if (function == null)
				context.ErrorListener.AddUndefinedFunctionError(FunctionName, Location);

			return function;
		}
	}
}
