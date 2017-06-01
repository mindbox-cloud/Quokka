using System;

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace Mindbox.Quokka.Generated
{
	internal partial class QuokkaBaseVisitor<Result>
	{
		protected VisitingContext VisitingContext { get; }

		protected Location GetLocationFromToken(IToken token)
		{
			return new Location(token.Line, token.Column);
		}

		protected QuokkaBaseVisitor(VisitingContext visitingContext)
		{
			VisitingContext = visitingContext;
		}
	}
}

