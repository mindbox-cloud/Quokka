namespace Quokka
{
	internal class IsEmptyTemplateFunction : TemplateFunction<string, bool>
	{
		public IsEmptyTemplateFunction()
			: base(
				  "isEmpty",
				  new TemplateFunctionArgument<string>("string"))
		{
		}

		public override bool Invoke(string argument)
		{
			return string.IsNullOrWhiteSpace(argument);
		}
	}
}
