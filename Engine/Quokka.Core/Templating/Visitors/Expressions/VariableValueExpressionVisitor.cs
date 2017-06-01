using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
    internal class VariableValueExpressionVisitor : QuokkaBaseVisitor<VariableValueExpression>
    {
	    public VariableValueExpressionVisitor(VisitingContext visitingContext) 
			: base(visitingContext)
	    {
	    }

	    public override VariableValueExpression VisitVariableValueExpression(QuokkaParser.VariableValueExpressionContext context)
	    {
		    return new VariableValueExpression(
			    context.Identifier().GetText(),
			    GetLocationFromToken(context.Identifier().Symbol));
	    }
    }
}
