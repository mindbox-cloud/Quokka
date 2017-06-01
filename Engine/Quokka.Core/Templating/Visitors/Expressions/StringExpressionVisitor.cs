using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
    internal class StringExpressionVisitor : QuokkaBaseVisitor<StringExpression>
    {
	    public StringExpressionVisitor(VisitingContext visitingContext) 
			: base(visitingContext)
	    {
	    }

	    public override StringExpression VisitStringConstant(QuokkaParser.StringConstantContext context)
	    {
			var quotedString = context.DoubleQuotedString()?.GetText() 
				?? context.SingleQuotedString().GetText();
		    string stringValue = quotedString.Substring(1, quotedString.Length - 2);

		    return new StringConstantExpression(stringValue);
		}
    }
}
