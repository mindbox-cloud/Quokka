using System;

namespace Quokka
{
	internal class FunctionCallBooleanExpression : BooleanExpressionBase
	{
		private readonly FunctionCall functionCall;

		public FunctionCallBooleanExpression(FunctionCall functionCall)
		{
			this.functionCall = functionCall;
		}

		public override bool Evaluate(RenderContext renderContext)
		{
			return (bool)functionCall.GetInvocationValue(renderContext);
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			var function = context.Functions.TryGetFunction(functionCall);
			if (function.ReturnType != typeof(bool))
			{
				context.ErrorListener.AddInvalidFunctionResultTypeError(
								function.Name, typeof(bool), function.ReturnType,
								functionCall.Location);
			}

			functionCall.CompileVariableDefinitions(context);
		}
	}
}
