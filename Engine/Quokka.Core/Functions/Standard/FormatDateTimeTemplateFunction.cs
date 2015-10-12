using System;

namespace Quokka
{
	internal class FormatDateTimeTemplateFunction : TemplateFunction<DateTime, string, string>
	{
		public FormatDateTimeTemplateFunction()
			: base(
				  "formatDateTime",
				  new TemplateFunctionArgument<DateTime>("dateTime"),
				  new TemplateFunctionArgument<string>("format", ValidateFormat))
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
				return new ArgumentValueValidationResult(false, "Строка формата имеет неверный формат");
			}
		}
	}
}