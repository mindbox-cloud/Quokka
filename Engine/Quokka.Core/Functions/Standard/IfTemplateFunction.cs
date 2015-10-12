namespace Quokka
{
	internal class IfTemplateFunction : TemplateFunction<bool, string, string, string>
	{
		public IfTemplateFunction()
			: base(
				  "if",
				  new TemplateFunctionArgument<bool>("condition"),
				  new TemplateFunctionArgument<string>("trueValue"),
				  new TemplateFunctionArgument<string>("falseValue"))
		{
		}

		public override string Invoke(bool argument1, string argument2, string argument3)
		{
			return argument1 ? argument2 : argument3;
		}
	}
}
