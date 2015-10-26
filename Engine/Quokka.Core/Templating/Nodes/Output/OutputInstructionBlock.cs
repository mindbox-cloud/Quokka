using System.Text;

namespace Quokka
{
	internal class OutputInstructionBlock : ITemplateNode
	{
		private readonly IOutputBlock outputBlock;
		
		public int Offset { get; }

		public OutputInstructionBlock(IOutputBlock outputBlock, int offset)
		{
			this.outputBlock = outputBlock;
			Offset = offset;
		}

		public void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			outputBlock.CompileVariableDefinitions(context);
		}

		public void Render(StringBuilder resultBuilder, RenderContext context)
		{
			outputBlock.Render(resultBuilder, context);
		}
	}
}
