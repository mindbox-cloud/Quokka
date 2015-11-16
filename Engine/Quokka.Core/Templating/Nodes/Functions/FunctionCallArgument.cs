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
		}

		public override object GetValue(RenderContext renderContext)
		{
			return functionCall.GetInvocationValue(renderContext);
		}
	}
}
