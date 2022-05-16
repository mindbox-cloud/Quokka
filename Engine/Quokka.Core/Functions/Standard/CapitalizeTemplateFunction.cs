using System;
using System.Linq;

namespace Mindbox.Quokka
{
	internal class CapitalizeTemplateFunction : ScalarTemplateFunction<string, string>
	{
		public CapitalizeTemplateFunction()
			: base("capitalize", new StringFunctionArgument("value"))
		{
		}

		public override string Invoke(string value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			var parts = value.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
				.Select((str, i) => i == 0 ? Capitalize(str) : str);

			return string.Join(" ", parts);
		}
		
		private static string Capitalize(string value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			if (value == string.Empty)
				return value;

			return value.Substring(0, 1).ToUpper() + value.Substring(1);
		}
	}
}
