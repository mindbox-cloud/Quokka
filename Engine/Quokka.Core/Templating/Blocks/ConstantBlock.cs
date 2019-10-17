using System.IO;

namespace Mindbox.Quokka
{
	internal class ConstantBlock : TemplateNodeBase, IStaticBlockPart
	{
		public string Text { get; }
		public int Offset { get; }
		public int Length { get; }

		public override bool IsConstant { get; } = true;

		public ConstantBlock(string text, int offset, int length)
		{
			Text = text;
			Offset = offset;
			Length = length;
		}

		public override void Render(TextWriter resultWriter, RenderContext renderContext)
		{
			resultWriter.Write(Text);
		}
	}
}
