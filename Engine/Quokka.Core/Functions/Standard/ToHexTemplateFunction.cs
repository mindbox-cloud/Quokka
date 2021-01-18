using System.Text;

namespace Mindbox.Quokka
{
	internal class ToHexTemplateFunction : ScalarTemplateFunction<string, string>
	{
		public ToHexTemplateFunction()
			: base(
				  "toHexadecimal",
				  new StringFunctionArgument("string"))
		{
		}

		public override string Invoke(string argument1)
		{
			return ByteUtility.ToHexString(Encoding.UTF8.GetBytes(argument1));
		}
	}
}
