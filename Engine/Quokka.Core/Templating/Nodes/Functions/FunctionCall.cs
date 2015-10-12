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
			var function = context.Functions.TryGetFunction(this);
			if (function == null)
				context.ErrorListener.AddUndefinedFunctionError(FunctionName, Location);
			
			for (int i = 0; i < arguments.Count; i++)
			{
				// Even if the function doesn't exist we want to try and analyze arguments further so we don't miss 
				// semantic errors that could be present there
				VariableType requiredType = VariableType.Unknown;
				if (function != null)
				{
					requiredType = VariableTypeTools.GetVariableTypeByRuntimeType(function.Arguments[i].RuntimeType);

					object staticValue;
					if (arguments[i].TryGetStaticValue(out staticValue))
					{
						var validationResult = function.Arguments[i].ValidateValue(staticValue);
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

				arguments[i].CompileVariableDefinitions(context, requiredType);
			}
		}

		public object GetInvocationValue(RenderContext renderContext)
		{
			var function = renderContext.Functions.TryGetFunction(this);
			if (function == null)
				throw new InvalidOperationException($"Function {FunctionName}");

			return function.Invoke(arguments.Select(arg => arg.GetValue(renderContext)).ToList());
		}
	}
}
