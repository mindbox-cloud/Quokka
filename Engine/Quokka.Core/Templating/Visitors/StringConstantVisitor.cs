using Quokka.Generated;

namespace Quokka
{
	internal class StringConstantVisitor : QuokkaBaseVisitor<string>
	{
		public static StringConstantVisitor Instance { get; } = new StringConstantVisitor();

		private StringConstantVisitor()
		{
		}

		public override string VisitStringConstant(QuokkaParser.StringConstantContext context)
		{
			var quotedString = context.DoubleQuotedString().GetText();
			return quotedString.Substring(1, quotedString.Length - 2);
		}
	}
}
