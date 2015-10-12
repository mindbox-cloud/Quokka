namespace Quokka
{
	internal class StringArgument : FunctionArgumentBase
	{
		private readonly string value;

		public StringArgument(string value)
		{
			this.value = value;
		}

		public override object GetValue(RenderContext renderContext)
		{
			return value;
		}
	}
}
