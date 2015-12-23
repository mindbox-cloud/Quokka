namespace Quokka
{
	internal class ToLowerTemplateFunction : ScalarTemplateFunction<string, string>
	{
		public ToLowerTemplateFunction()
			: base(
				  "toLower",
				  new StringFunctionArgument("string"))
		{
		}

		public override string Invoke(string argument1)
		{
			return argument1.ToLower();
		}
	}
}
