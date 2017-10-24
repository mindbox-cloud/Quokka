using System;

namespace Mindbox.Quokka
{
	public class CeilingTemplateFunction : ScalarTemplateFunction<decimal, decimal>
	{
		public CeilingTemplateFunction()
			: base("ceiling",
				new DecimalFunctionArgument("value"))
		{
		}

		public override decimal Invoke(decimal value)
		{
			return Math.Ceiling(value);
		}
	}
}