using System;
using System.IO;
using System.Text;

namespace Mindbox.Quokka
{
	internal class OutputInstructionBlock : TemplateNodeBase, IStaticBlockPart
	{
		private readonly IExpression expression;
		
		public int Offset { get; }
		public int Length { get; }

		public OutputInstructionBlock(IExpression expression, int offset, int length)
		{
			this.expression = expression;
			Offset = offset;
			Length = length;
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			expression.PerformSemanticAnalysis(context, TypeDefinition.Primitive);
		}

		public override void Render(TextWriter resultWriter, RenderContext renderContext)
		{
			resultWriter.Write(expression.GetOutputValue(renderContext));
		}
	}
}
