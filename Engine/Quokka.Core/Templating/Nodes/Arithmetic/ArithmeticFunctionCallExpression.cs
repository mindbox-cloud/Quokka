using System;

namespace Quokka
{
	internal class ArithmeticFunctionCallExpression : ArithmeticExpressionBase
	{
		private readonly FunctionCall functionCall;

		public ArithmeticFunctionCallExpression(FunctionCall functionCall)
		{
			this.functionCall = functionCall;
		}

		public override double GetValue(RenderContext renderContext)
		{
			return Convert.ToDouble(functionCall.GetInvocationResult(renderContext).GetPrimitiveValue());
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			var function = functionCall.TryGetFunctionForSemanticAnalysis(context);
			if (function == null)
				return;

			var functionResultType = TypeDefinition.GetTypeDefinitionFromModelDefinition(function.ReturnValueDefinition);

			if (!functionResultType.IsCompatibleWithRequired(TypeDefinition.Decimal))
			{
				context.ErrorListener.AddInvalidFunctionResultTypeError(
								function.Name,
								TypeDefinition.Decimal,
								functionResultType,
								functionCall.Location);
			}

			functionCall.CompileVariableDefinitions(context);
		}
	}
}
