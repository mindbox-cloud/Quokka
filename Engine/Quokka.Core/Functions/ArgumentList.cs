using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	public class ArgumentList
	{
		private readonly TemplateFunction function;

		internal IList<TemplateFunctionArgument> Arguments { get; }

		public ArgumentList(TemplateFunction function, IEnumerable<TemplateFunctionArgument> arguments)
		{
			this.function = function;
			Arguments = arguments.ToList();
		}

		internal void AnalyzeArgumentValuesBasedOnFunctionResultUsages(
			AnalysisContext context, 
			IReadOnlyList<ArgumentValue> argumentValues,
			ValueUsageSummary resultDefinition)
		{
			if (argumentValues.Count != Arguments.Count)
				return;

			for (int i = 0; i < argumentValues.Count; i++)
				Arguments[i].AnalyzeArgumentValueBasedOnFunctionResultUsages(context, resultDefinition, argumentValues[i].Expression);
		}

		internal void PerformSemanticAnalysis(
			AnalysisContext context, 
			IReadOnlyList<ArgumentValue> arguments,
			Location location)
		{
			if (!CheckArgumentNumber(arguments))
			{
				context.ErrorListener.AddInvalidFunctionArgumentCountError(function.Name,
					new []{Arguments.Count},
					arguments.Count,
					location);
			}
			else
			{
				for (int i = 0; i < arguments.Count; i++)
				{
					var requiredType = GetRequiredType(i);
					CheckArgument(context, arguments, location, i, requiredType);
					arguments[i].PerformSemanticAnalysis(context, requiredType);
				}
			}
		}

		internal virtual bool CheckArgumentNumber(IReadOnlyList<ArgumentValue> arguments)
		{
			return Arguments.Count == arguments.Count;
		}

		private void CheckArgument(
			AnalysisContext context,
			IReadOnlyList<ArgumentValue> arguments,
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
						var validationResult = GetArgument(argumentNumber).ValidateConstantValue(staticValue);
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

		internal virtual TemplateFunctionArgument GetArgument(int argumentNumber)
		{
			return Arguments[argumentNumber];
		}

		protected virtual TypeDefinition GetRequiredType(int argumentNumber)
		{
			return Arguments[argumentNumber].Type;
		}
	}
}