namespace Quokka
{
	internal class ToUpperTemplateFunction : TemplateFunction<string, string>
	{
		public ToUpperTemplateFunction()
			: base("toUpper")
		{
		}

		public override string Invoke(string argument1)
		{
			return argument1.ToUpper();
		}
	}
}
