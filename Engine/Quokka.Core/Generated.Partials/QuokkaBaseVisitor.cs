using Antlr4.Runtime;

namespace Quokka.Generated
{
	internal partial class QuokkaBaseVisitor<Result>
	{
		protected readonly VisitingContext visitingContext;

		protected Location GetLocationFromToken(IToken token)
		{
			return new Location(token.Line, token.Column);
		}

		protected QuokkaBaseVisitor(VisitingContext visitingContext)
		{
			this.visitingContext = visitingContext;
		}
	}
}

