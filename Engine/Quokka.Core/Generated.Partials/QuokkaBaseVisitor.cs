using Antlr4.Runtime;

namespace Quokka.Generated
{
	internal partial class QuokkaBaseVisitor<Result>
	{
		protected Location GetLocationFromToken(IToken token)
		{
			return new Location(token.Line, token.Column);
		}
	}
}

