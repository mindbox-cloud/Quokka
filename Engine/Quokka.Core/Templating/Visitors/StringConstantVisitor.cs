using Quokka.Generated;

namespace Quokka
{
	internal class StringConstantVisitor : QuokkaBaseVisitor<string>
	{
		public StringConstantVisitor(VisitingContext visitingContext)
			: base(visitingContext)
		{
		}

		public override string VisitStringConstant(QuokkaParser.StringConstantContext context)
		{
			var quotedString = context.DoubleQuotedString()?.GetText() ?? context.SingleQuotedString().GetText();
			return quotedString.Substring(1, quotedString.Length - 2);
		}
	}
}
