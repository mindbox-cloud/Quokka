namespace Quokka
{
	internal class ConstantBlock : TemplateNodeBase
	{
		private readonly string text;

		public ConstantBlock(string text)
		{
			this.text = text;
		}
	}
}
