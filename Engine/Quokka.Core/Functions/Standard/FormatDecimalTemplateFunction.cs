using System;

namespace Quokka
{
	internal class FormatDecimalTemplateFunction : TemplateFunction<decimal, string, string>
	{
		public FormatDecimalTemplateFunction()
			: base(
				  "formatDecimal",
				  new DecimalFunctionArgument("number"), 
				  new StringFunctionArgument("format", ValidateFormat))
		{
		}

		public override string Invoke(decimal argument1, string argument2)
		{
			return argument1.ToString(argument2);
		}

		private static ArgumentValueValidationResult ValidateFormat(string format)
		{
			try
			{
				default(decimal).ToString(format);
				return new ArgumentValueValidationResult(true);
			}
			catch (FormatException)
			{
				return new ArgumentValueValidationResult(false, "Строка формата имеет неверный формат");
			}
		}
	}
}
