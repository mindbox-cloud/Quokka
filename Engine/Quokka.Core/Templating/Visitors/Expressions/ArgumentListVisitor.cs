using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
    internal class ArgumentListVisitor : QuokkaBaseVisitor<IEnumerable<Argument>>
    {
	    public ArgumentListVisitor(VisitingContext visitingContext) 
			: base(visitingContext)
	    {
	    }

	    public override IEnumerable<Argument> VisitArgumentList(QuokkaParser.ArgumentListContext context)
	    {
		    var expressionVisitor = new ExpressionVisitor(VisitingContext);

		    return context
			    .expression()
			    .Select(
				    argumentExpression => new Argument(
					    argumentExpression.Accept(expressionVisitor),
					    GetLocationFromToken(argumentExpression.Start)));
	    }
    }
}
