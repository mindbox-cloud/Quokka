using System;
using System.Text;

namespace Quokka
{
	internal class FunctionCallOutputBlock : TemplateNodeBase, IOutputBlock
	{
		private readonly FunctionCall functionCall;

		public FunctionCallOutputBlock(FunctionCall functionCall)
		{
			this.functionCall = functionCall;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			functionCall.CompileVariableDefinitions(context);
		}

		public override void Render(StringBuilder resultBuilder, RenderContext context)
		{
			var value = functionCall.GetInvocationValue(context);
			resultBuilder.Append(value);
		}
	}
}
