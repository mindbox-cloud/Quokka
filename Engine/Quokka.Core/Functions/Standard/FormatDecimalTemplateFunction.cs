namespace Quokka
{
	internal class FormatDecimalTemplateFunction : TemplateFunction<decimal, string, string>
	{
		public FormatDecimalTemplateFunction()
			: base(
				  "formatDecimal",
				  new TemplateFunctionArgument<decimal>("number"),
				  new TemplateFunctionArgument<string>("format"))
		{
		}

		public override string Invoke(decimal argument1, string argument2)
		{
			return argument1.ToString(argument2);
		}
	}
}
