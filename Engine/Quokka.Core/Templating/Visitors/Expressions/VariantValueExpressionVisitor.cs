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
