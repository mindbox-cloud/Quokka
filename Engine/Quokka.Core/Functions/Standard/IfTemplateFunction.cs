namespace Quokka
{
	internal class IfTemplateFunction : ScalarTemplateFunction<bool, string, string, string>
	{
		public IfTemplateFunction()
			: base(
				  "if",
				  new BoolFunctionArgument("condition"), 
				  new StringFunctionArgument("trueValue"),
				  new StringFunctionArgument("falseValue"))
		{
		}

		public override string Invoke(bool argument1, string argument2, string argument3)
		{
			return argument1 ? argument2 : argument3;
		}
	}
}
