using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
    internal class ExpressionVisitor : QuokkaBaseVisitor<IExpression>
    {
	    public ExpressionVisitor(VisitingContext visitingContext) 
			: base(visitingContext)
	    {
	    }

	    public override IExpression VisitVariantValueExpression(QuokkaParser.VariantValueExpressionContext context)
	    {
		    return context.Accept(new VariantValueExpressionVisitor(VisitingContext));
	    }

	    public override IExpression VisitStringExpression(QuokkaParser.StringExpressionContext context)
	    {
		    return context.Accept(new StringExpressionVisitor(VisitingContext));
	    }

	    public override IExpression VisitBooleanExpression(QuokkaParser.BooleanExpressionContext context)
	    {
		    return context.Accept(new BooleanExpressionVisitor(VisitingContext));
	    }

	    public override IExpression VisitArithmeticExpression(QuokkaParser.ArithmeticExpressionContext context)
	    {
		    return context.Accept(new ArithmeticExpressionVisitor(VisitingContext));
	    }
	}
}
