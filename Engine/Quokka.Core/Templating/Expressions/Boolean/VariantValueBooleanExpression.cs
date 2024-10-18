// // Copyright 2022 Mindbox Ltd
// //
// // Licensed under the Apache License, Version 2.0 (the "License");
// // you may not use this file except in compliance with the License.
// // You may obtain a copy of the License at
// //
// //     http://www.apache.org/licenses/LICENSE-2.0
// //
// // Unless required by applicable law or agreed to in writing, software
// // distributed under the License is distributed on an "AS IS" BASIS,
// // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// // See the License for the specific language governing permissions and
// // limitations under the License.

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

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			return variantValueExpression.CheckIfExpressionIsNull(renderContext);
		}

		public override void Accept(ITreeVisitor treeVisitor)
		{
			treeVisitor.VisitVariantValueBooleanExpression();
			
			variantValueExpression.Accept(treeVisitor);
			
			treeVisitor.EndVisit();
		}
	}
}
