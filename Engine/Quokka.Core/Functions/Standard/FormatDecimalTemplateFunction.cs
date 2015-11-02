using System;

namespace Quokka
{
	internal class FormatDecimalTemplateFunction : TemplateFunction<decimal?, string, string>
	{
		public FormatDecimalTemplateFunction()
			: base(
				  "formatDecimal",
				  new TemplateFunctionArgument<decimal?>("number"),
				  new TemplateFunctionArgument<string>("format", ValidateFormat))
		{
		}

		public override string Invoke(decimal? argument1, string argument2)
		{
			return argument1 == null ? "" : argument1.Value.ToString(argument2);
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
