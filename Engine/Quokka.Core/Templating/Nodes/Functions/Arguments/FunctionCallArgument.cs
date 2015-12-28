using System;

namespace Quokka
{
	internal class FunctionCallArgument : FunctionArgumentBase
	{
		private readonly FunctionCall functionCall;

		public FunctionCallArgument(FunctionCall functionCall, Location location)
			: base(location)
		{
			this.functionCall = functionCall;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context, TypeDefinition requiredArgumentType)
		{
			functionCall.CompileVariableDefinitions(context);
			var function = context.Functions.TryGetFunction(functionCall);
			if (function != null)
			{
				var returnType = TypeDefinition.GetTypeDefinitionFromModelDefinition(function.ReturnValueDefinition);
				if (!returnType.IsCompatibleWithRequired(requiredArgumentType))
					context.ErrorListener.AddInvalidFunctionResultTypeError(
						functionCall.FunctionName,
						requiredArgumentType,
						returnType,
						functionCall.Location);
			}
		}

		public override VariableValueStorage GetValue(RenderContext renderContext)
		{
			return functionCall.GetInvocationResult(renderContext);
		}
	}
}
