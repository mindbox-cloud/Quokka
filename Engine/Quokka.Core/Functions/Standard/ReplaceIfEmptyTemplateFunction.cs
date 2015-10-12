namespace Quokka
{
	internal class ReplaceIfEmptyTemplateFunction : TemplateFunction<string, string, string>
	{
		public ReplaceIfEmptyTemplateFunction()
			: base(
				  "replaceIfEmpty",
				  new TemplateFunctionArgument<string>("default value"),
				  new TemplateFunctionArgument<string>("fallback value"))
		{
		}

		public override string Invoke(string argument1, string argument2)
		{
			return !string.IsNullOrWhiteSpace(argument1) ? argument1 : argument2;
		}
	}
}
