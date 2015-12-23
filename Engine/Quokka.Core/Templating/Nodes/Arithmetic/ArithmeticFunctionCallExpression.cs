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
			functionCall.CompileVariableDefinitions(context);
		}
	}
}
