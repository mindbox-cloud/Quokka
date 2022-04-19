using System;
using System.Globalization;

namespace Mindbox.Quokka
{
	internal class FormatDecimalTemplateFunction : ScalarTemplateFunction<decimal, string, string>
	{
		public FormatDecimalTemplateFunction()
			: base(
				  "formatDecimal",
				  new DecimalFunctionArgument("number"), 
				  new StringFunctionArgument("format", valueValidator: ValidateFormat))
		{
		}

		public override string Invoke(decimal argument1, string argument2)
		{
			return argument1.ToString(argument2, CultureInfo.CurrentCulture);
		}

		private static ArgumentValueValidationResult ValidateFormat(string format)
		{
			try
			{
				default(decimal).ToString(format, CultureInfo.CurrentCulture);
				return new ArgumentValueValidationResult(true);
			}
			catch (FormatException)
			{
				return new ArgumentValueValidationResult(false, "Format string is invalid");
			}
		}
	}
}
