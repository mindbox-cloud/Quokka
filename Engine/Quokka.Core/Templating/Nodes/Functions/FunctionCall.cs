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

			if (function == null)
			{
				context.ErrorListener.AddUndefinedFunctionError(FunctionName, Location);
			}
			else if (function.Arguments.Count != arguments.Count)
			{
				context.ErrorListener.AddInvalidFunctionArgumentCountError(FunctionName,
					function.Arguments.Count,
					arguments.Count,
					Location);
			}
			else
			{
				for (int i = 0; i < arguments.Count; i++)
				{
					var requiredType = function.Arguments[i].Type;
					var staticArgumentType = arguments[i].TryGetStaticType(context);
					if (staticArgumentType != null)
					{
						if (!staticArgumentType.IsCompatibleWithRequired(requiredType))
						{
							context.ErrorListener.AddInvalidFunctionArgumentTypeError(
								function.Name,
								function.Arguments[i].Name,
								staticArgumentType,
								requiredType,
								Location);
						}
						else
						{
							object staticValue;
							if (arguments[i].TryGetStaticValue(out staticValue))
							{
								var validationResult = function.Arguments[i].ValidateValue(new PrimitiveVariableValueStorage(staticValue));
								if (!validationResult.IsValid)
								{
									context.ErrorListener.AddInvalidFunctionArgumentValueError(
										function.Name,
										function.Arguments[i].Name,
										validationResult.ErrorMessage,
										Location);
								}
							}
						}
					}

					arguments[i].CompileVariableDefinitions(context, requiredType);
				}
			}
		}
		
		public void MapArgumentVariableDefinitionsToResult(
			SemanticAnalysisContext context,
			VariableDefinition resultDefinition)
		{
			var function = TryGetFunctionForSemanticAnalysis(context);

			// We only do this if the template is semantically valid. If not, semantic errors are already handled
			// earlier in the workflow.
			if (function != null && arguments.Count == function.Arguments.Count)
			{
				for (int i = 0; i < arguments.Count; i++)
					arguments[i].MapArgumentVariableDefinitionsToResult(context, resultDefinition, function.Arguments[i]);
			}
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
