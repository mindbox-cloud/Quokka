namespace Quokka
{
	internal class StringArgument : FunctionArgumentBase
	{
		private readonly string value;

		public StringArgument(string value, Location location)
			: base(location)
		{
			this.value = value;
		}

		public override object GetValue(RenderContext renderContext)
		{
			return value;
		}

		public override bool TryGetStaticValue(out object staticValue)
		{
			staticValue = value;
			return true;
		}
	}
}
