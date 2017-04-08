using System.Text;

namespace Mindbox.Quokka
{
	internal class StringConstantOutputBlock : TemplateNodeBase, IOutputBlock
	{
		private readonly string stringValue;

		public StringConstantOutputBlock(string stringValue)
		{
			this.stringValue = stringValue;
		}

		public override void Render(StringBuilder resultBuilder, RenderContext context)
		{
			resultBuilder.Append(stringValue);
		}
	}
}
