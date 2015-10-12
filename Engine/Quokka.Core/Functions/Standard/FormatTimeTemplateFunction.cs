using System;

namespace Quokka
{
	internal class FormatTimeTemplateFunction : TemplateFunction<TimeSpan, string, string>
	{
		public FormatTimeTemplateFunction()
			: base(
				  "formatTime",
				  new TemplateFunctionArgument<TimeSpan>("time"),
				  new TemplateFunctionArgument<string>("format", ValidateFormat))
		{
		}

		public override string Invoke(TimeSpan argument1, string argument2)
		{
			return argument1.ToString(argument2);
		}

		private static ArgumentValueValidationResult ValidateFormat(string format)
		{
			try
			{
				default(TimeSpan).ToString(format);
				return new ArgumentValueValidationResult(true);
			}
			catch (FormatException)
			{
				return new ArgumentValueValidationResult(false, "Строка формата имеет неверный формат");
			}
		}
	}
}
