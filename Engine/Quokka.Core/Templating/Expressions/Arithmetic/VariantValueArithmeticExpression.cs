using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
    internal class VariantValueArithmeticExpression : ArithmeticExpression
    {
	    private readonly VariantValueExpression variantValueExpression;

	    public VariantValueArithmeticExpression(VariantValueExpression variantValueExpression)
	    {
		    this.variantValueExpression = variantValueExpression;
	    }

	    public override TypeDefinition GetResultType(AnalysisContext context)
	    {
		    var expressionType = variantValueExpression.GetResultType(context);
		    return expressionType == TypeDefinition.Unknown 
				? TypeDefinition.Decimal 
				: expressionType;
	    }

	    public override double GetValue(RenderContext renderContext)
	    {
			return Convert.ToDouble(variantValueExpression.Evaluate(renderContext).GetPrimitiveValue());
		}

	    public override void PerformSemanticAnalysis(AnalysisContext context)
	    {
		    variantValueExpression.PerformSemanticAnalysis(context, TypeDefinition.Decimal);
	    }
    }
}
