namespace Quokka
{
	internal class IsEmptyTemplateFunction : TemplateFunction<string, bool>
	{
		public IsEmptyTemplateFunction()
			: base(
				  "isEmpty",
				  new StringFunctionArgument("string"))
		{
		}

		public override bool Invoke(string argument)
		{
			return string.IsNullOrWhiteSpace(argument);
		}
	}
}
