using System;

namespace Mindbox.Quokka
{
	internal class FormatTimeTemplateFunction : ScalarTemplateFunction<TimeSpan, string, string>
	{
		public FormatTimeTemplateFunction()
			: base(
				  "formatTime",
				  new TimeSpanFunctionArgument("time"),
				  new StringFunctionArgument("format", ValidateFormat))
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
