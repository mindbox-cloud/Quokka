using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace Quokka.Generated
{
	internal partial class QuokkaBaseVisitor<Result> : AbstractParseTreeVisitor<Result>, IQuokkaVisitor<Result>
	{
		protected Location GetLocationFromToken(IToken token)
		{
			return new Location(token.Line, token.Column);
		}
	}
}

