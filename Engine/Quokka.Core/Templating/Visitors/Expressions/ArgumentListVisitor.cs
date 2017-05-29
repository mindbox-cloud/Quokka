using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
    internal class ArgumentListVisitor : QuokkaBaseVisitor<IEnumerable<ArgumentValue>>
    {
	    public ArgumentListVisitor(VisitingContext visitingContext) 
			: base(visitingContext)
	    {
	    }

	    public override IEnumerable<ArgumentValue> VisitArgumentList(QuokkaParser.ArgumentListContext context)
	    {
		    var expressionVisitor = new ExpressionVisitor(VisitingContext);

		    return context
			    .expression()
			    .Select(
				    argumentExpression => new ArgumentValue(
					    argumentExpression.Accept(expressionVisitor),
					    GetLocationFromToken(argumentExpression.Start)));
	    }
    }
}
