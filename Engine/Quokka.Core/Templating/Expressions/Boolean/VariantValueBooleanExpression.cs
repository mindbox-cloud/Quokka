using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
    internal class VariantValueBooleanExpression : BooleanExpression
    {
	    private readonly VariantValueExpression variantValueExpression;

	    public VariantValueBooleanExpression(VariantValueExpression variantValueExpression)
	    {
		    this.variantValueExpression = variantValueExpression;
	    }

	    public override bool GetBooleanValue(RenderContext renderContext)
	    {
		    var value = variantValueExpression.Evaluate(renderContext);
		    var booleanValue = (bool)value.GetPrimitiveValue();
		    return booleanValue;
	    }

	    public override void PerformSemanticAnalysis(AnalysisContext context)
	    {
		    variantValueExpression.PerformSemanticAnalysis(context, TypeDefinition.Boolean);
	    }
    }
}
