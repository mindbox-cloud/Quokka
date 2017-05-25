using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
	internal class Argument
	{
		private readonly IExpression expression;

		public Location Location { get; }

		public Argument(IExpression expression, Location location)
		{
			this.expression = expression;
			Location = location;
		}

		public void CompileVariableDefinitions(SemanticAnalysisContext context, TypeDefinition requiredArgumentType)
		{
			expression.CompileVariableDefinitions(context, requiredArgumentType);
		}

		public TypeDefinition GetStaticType(SemanticAnalysisContext context)
		{
			return expression.GetResultType(context);
		}

		public VariableValueStorage GetValue(RenderContext renderContext)
		{
			return expression.Evaluate(renderContext);
		}

		public VariableValueStorage TryGetStaticValue()
		{
			return expression.TryGetStaticEvaluationResult();
		}

		public void AnalyzeArgumentValueBasedOnFunctionResultUsages(
			SemanticAnalysisContext context,
			VariableDefinition resultDefinition,
			TemplateFunctionArgument functionArgument)
		{
			functionArgument.AnalyzeArgumentValueBasedOnFunctionResultUsages(context, resultDefinition, expression);
		}
	}
}
