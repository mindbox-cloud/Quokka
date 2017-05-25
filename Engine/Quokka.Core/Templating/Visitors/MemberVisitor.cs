using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
    internal class MemberVisitor : QuokkaBaseVisitor<Member>
    {
	    public MemberVisitor(VisitingContext visitingContext) 
			: base(visitingContext)
	    {
	    }

	    public override Member VisitField(QuokkaParser.FieldContext context)
	    {
		    return new FieldMember(
			    context.Identifier().GetText(),
			    GetLocationFromToken(context.Identifier().Symbol));
	    }

	    public override Member VisitMethodCall(QuokkaParser.MethodCallContext context)
	    {
		    throw new NotImplementedException();
	    }
    }
}
