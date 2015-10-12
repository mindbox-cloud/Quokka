namespace Quokka
{
	internal class ReplaceIfEmptyTemplateFunction : TemplateFunction<string, string, string>
	{
		public ReplaceIfEmptyTemplateFunction()
			: base("replaceIfEmpty")
		{
		}

		public override string Invoke(string argument1, string argument2)
		{
			return !string.IsNullOrWhiteSpace(argument1) ? argument1 : argument2;
		}
	}
}
