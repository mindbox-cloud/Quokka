using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
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

		public override StringExpression VisitStringConcatenation(QuokkaParser.StringConcatenationContext context)
		{
			var firstOperand = 
				context.stringAtom().variantValueExpression()?.Accept(new ExpressionVisitor(VisitingContext))
					?? context.stringAtom().stringConstant().Accept(this);

			return new StringConcatenationExpression(
					firstOperand,
					context.expression().Accept(new ExpressionVisitor(VisitingContext))
				);
		}
	}
}
