using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
    internal class VariantValueExpressionVisitor : QuokkaBaseVisitor<VariantValueExpression>
    {
	    public VariantValueExpressionVisitor(VisitingContext visitingContext) 
			: base(visitingContext)
	    {
	    }
		
	    public override VariantValueExpression VisitVariableValueExpression(QuokkaParser.VariableValueExpressionContext context)
	    {
		    return context.Accept(new VariableValueExpressionVisitor(VisitingContext));
	    }

	    public override VariantValueExpression VisitFunctionCallExpression(QuokkaParser.FunctionCallExpressionContext context)
	    {
		    return context.Accept(new FunctionCallExpressionVisitor(VisitingContext));
	    }

	    public override VariantValueExpression VisitMemberValueExpression(QuokkaParser.MemberValueExpressionContext context)
	    {
		    var memberVisitor = new MemberVisitor(VisitingContext);

			var rootExpression = context.variableValueExpression()
				.Accept(new VariableValueExpressionVisitor(VisitingContext));

		    var members = context.member()
			    .Select(memberRule => memberRule.Accept(memberVisitor))
			    .ToArray();

		    return new MemberValueExpression(rootExpression, members);
	    }
    }
}
