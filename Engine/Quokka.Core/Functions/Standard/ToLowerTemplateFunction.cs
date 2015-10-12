namespace Quokka
{
	internal class ToLowerTemplateFunction : TemplateFunction<string, string>
	{
		public ToLowerTemplateFunction()
			: base("toLower")
		{
		}

		public override string Invoke(string argument1)
		{
			return argument1.ToLower();
		}
	}
}
