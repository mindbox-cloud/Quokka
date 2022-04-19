using System;

namespace Mindbox.Quokka
{
	internal class FormatDateTimeTemplateFunction : ScalarTemplateFunction<DateTime, string, string>
	{
		public FormatDateTimeTemplateFunction()
			: base(
				  "formatDateTime",
				  new DateTimeFunctionArgument("dateTime"), 
				  new StringFunctionArgument("format", valueValidator: ValidateFormat))
		{
		}

		public override string Invoke(DateTime argument1, string argument2)
		{
			return argument1.ToString(argument2);
		}

		private static ArgumentValueValidationResult ValidateFormat(string format)
		{
			try
			{
				default(DateTime).ToString(format);
				return new ArgumentValueValidationResult(true);
			}
			catch (FormatException)
			{
				return new ArgumentValueValidationResult(false, "Format string is invalid");
			}
		}
	}
}