using System.Globalization;

namespace Mindbox.Quokka
{
	internal class CapitalizeAllWordsTemplateFunction : ScalarTemplateFunction<string, string>
	{
		public CapitalizeAllWordsTemplateFunction()
			: base("capitalizeAllWords", new StringFunctionArgument("value"))
		{
		}

		public override string Invoke(string value)
		{
			return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value);
		}
	}
}