using System.Globalization;

namespace Mindbox.Quokka
{
	internal class ToUpperTemplateFunction : ScalarTemplateFunction<string, string>
	{
		public ToUpperTemplateFunction()
			: base(
				  "toUpper",
				  new StringFunctionArgument("string"))
		{
		}

		public override string Invoke(string argument1)
		{
			return argument1.ToUpper(CultureInfo.CurrentCulture);
		}
	}
}
