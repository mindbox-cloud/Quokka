namespace Quokka
{
	internal class FunctionCallArgument : FunctionArgumentBase
	{
		private readonly FunctionCall functionCall;

		public FunctionCallArgument(FunctionCall functionCall)
		{
			this.functionCall = functionCall;
		}

		public override object GetValue(RenderContext renderContext)
		{
			return functionCall.GetInvocationValue(renderContext);
		}
	}
}
