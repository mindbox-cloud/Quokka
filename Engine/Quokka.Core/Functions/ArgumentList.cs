using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	public class ArgumentList
	{
		private readonly TemplateFunction function;

		protected IList<TemplateFunctionArgument> Arguments { get; }

		public ArgumentList(TemplateFunction function, IEnumerable<TemplateFunctionArgument> arguments)
		{
			this.function = function;
			Arguments = arguments.ToList();
		}

		internal void MapArgumentVariableDefinitionsToResult(
			SemanticAnalysisContext context, 
			IReadOnlyList<Argument> arguments,
			VariableDefinition resultDefinition)
		{
			if (arguments.Count != Arguments.Count)
				return;

			for (int i = 0; i < arguments.Count; i++)
				arguments[i].AnalyzeArgumentValueBasedOnFunctionResultUsages(context, resultDefinition, Arguments[i]);
		}

		internal void CompileVariableDefinitions(
			SemanticAnalysisContext context, 
			IReadOnlyList<Argument> arguments,
			Location location)
		{
			if (!CheckArgumentNumber(arguments))
			{
				context.ErrorListener.AddInvalidFunctionArgumentCountError(function.Name,
					Arguments.Count,
					arguments.Count,
					location);
			}
			else
			{
				for (int i = 0; i < arguments.Count; i++)
				{
					var requiredType = GetRequiredType(i);
					CheckArgument(context, arguments, location, i, requiredType);
					arguments[i].CompileVariableDefinitions(context, requiredType);
				}
			}
		}

		internal virtual bool CheckArgumentNumber(IReadOnlyList<Argument> arguments)
		{
			return Arguments.Count == arguments.Count;
		}

		private void CheckArgument(
			SemanticAnalysisContext context,
			IReadOnlyList<Argument> arguments,
			Location location,
			int argumentNumber,
			TypeDefinition requiredType)
		{
			var staticArgumentType = arguments[argumentNumber].GetStaticType(context);
			if (staticArgumentType != TypeDefinition.Unknown)
			{
				if (!staticArgumentType.IsAssignableTo(requiredType))
				{
					context.ErrorListener.AddInvalidFunctionArgumentTypeError(
						function.Name,
						GetArgument(argumentNumber).Name,
						staticArgumentType,
						requiredType,
						location);
				}
				else
				{
					var staticValue = arguments[argumentNumber].TryGetStaticValue();
					if (staticValue != null)
					{
						var validationResult = GetArgument(argumentNumber).ValidateValue(staticValue);
						if (!validationResult.IsValid)
						{
							context.ErrorListener.AddInvalidFunctionArgumentValueError(
								function.Name,
								GetArgument(argumentNumber).Name,
								validationResult.ErrorMessage,
								location);
						}
					}
				}
			}
		}

		protected virtual TemplateFunctionArgument GetArgument(int argumentNumber)
		{
			return Arguments[argumentNumber];
		}

		protected virtual TypeDefinition GetRequiredType(int argumentNumber)
		{
			return Arguments[argumentNumber].Type;
		}
	}
}