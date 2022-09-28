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
