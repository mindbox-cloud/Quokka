using System;

namespace Mindbox.Quokka
{
	public class FloorTemplateFunction : ScalarTemplateFunction<decimal, decimal>
	{
		public FloorTemplateFunction()
			: base("floor",
				new DecimalFunctionArgument("value"))
		{
		}

		public override decimal Invoke(decimal value)
		{
			return Math.Floor(value);
		}
	}
}