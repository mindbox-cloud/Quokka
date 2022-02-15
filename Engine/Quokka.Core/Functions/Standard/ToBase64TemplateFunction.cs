using System;
using System.Text;

namespace Mindbox.Quokka
{
	internal class ToBase64TemplateFunction : ScalarTemplateFunction<string, string>
	{
		public ToBase64TemplateFunction()
			: base(
				  "toBase64",
				  new StringFunctionArgument("string"))
		{
		}

		public override string Invoke(string argument1)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(argument1));
		}
	}
}
