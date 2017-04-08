using System.Text;

namespace Mindbox.Quokka
{
	internal class OutputInstructionBlock : TemplateNodeBase, IStaticBlockPart
	{
		private readonly IOutputBlock outputBlock;
		
		public int Offset { get; }
		public int Length { get; }

		public OutputInstructionBlock(IOutputBlock outputBlock, int offset, int length)
		{
			this.outputBlock = outputBlock;
			Offset = offset;
			Length = length;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			outputBlock.CompileVariableDefinitions(context);
		}

		public override void Render(StringBuilder resultBuilder, RenderContext context)
		{
			outputBlock.Render(resultBuilder, context);
		}
	}
}
