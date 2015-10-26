using System.Text;

namespace Quokka
{
	internal class ConstantBlock : TemplateNodeBase, IStaticBlockPart
	{
		public string Text { get; }
		public int Offset { get; }
		public int Length { get; }

		public ConstantBlock(string text, int offset, int length)
		{
			Text = text;
			Offset = offset;
			Length = length;
		}

		public override void Render(StringBuilder resultBuilder, RenderContext context)
		{
			resultBuilder.Append(Text);
		}

		
	}
}
