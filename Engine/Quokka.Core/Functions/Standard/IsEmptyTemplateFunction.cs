namespace Mindbox.Quokka
{
	internal class IsEmptyTemplateFunction : ScalarTemplateFunction<string, bool>
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