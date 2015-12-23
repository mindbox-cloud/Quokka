namespace Quokka
{
	internal class ReplaceIfEmptyTemplateFunction : ScalarTemplateFunction<string, string, string>
	{
		public ReplaceIfEmptyTemplateFunction()
			: base(
				  "replaceIfEmpty",
				  new StringFunctionArgument("default value"),
				  new StringFunctionArgument("fallback value"))
		{
		}

		public override string Invoke(string argument1, string argument2)
		{
			return !string.IsNullOrWhiteSpace(argument1) ? argument1 : argument2;
		}
	}
}
