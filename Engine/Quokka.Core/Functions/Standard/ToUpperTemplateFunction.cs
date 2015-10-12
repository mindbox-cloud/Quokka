namespace Quokka
{
	internal class ToUpperTemplateFunction : TemplateFunction<string, string>
	{
		public ToUpperTemplateFunction()
			: base(
				  "toUpper",
				  new TemplateFunctionArgument<string>("string"))
		{
		}

		public override string Invoke(string argument1)
		{
			return argument1.ToUpper();
		}
	}
}
