using System;

namespace Quokka
{
	internal class AddDaysTemplateFunction : ScalarTemplateFunction<DateTime, decimal, DateTime>
	{
		public AddDaysTemplateFunction()
			: base(
				  "addDays",
				  new DateTimeFunctionArgument("date"), 
				  new DecimalFunctionArgument("daysAmount"))
		{
		}

		public override DateTime Invoke(DateTime date, decimal daysAmount)
		{
			return date.AddDays((double)daysAmount);
		}
	}
}
