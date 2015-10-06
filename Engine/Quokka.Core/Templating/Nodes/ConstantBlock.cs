using System.Text;

namespace Quokka
{
	internal class ConstantBlock : TemplateNodeBase
	{
		private readonly string text;

		public ConstantBlock(string text)
		{
			this.text = text;
		}

		public override void Render(StringBuilder resultBuilder, VariableValueStorage valueStorage)
		{
			resultBuilder.Append(text);
		}
	}
}
