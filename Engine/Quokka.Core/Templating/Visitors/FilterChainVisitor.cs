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

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class FilterChainVisitor : QuokkaBaseVisitor<FunctionCallExpression>
	{
		public FilterChainVisitor(VisitingContext visitingContext)
			: base(visitingContext)
		{
		}

		public override FunctionCallExpression VisitOutputBlock(QuokkaParser.OutputBlockContext context)
		{
			var implicitlyPassedArgument = new ArgumentValue(
				context.expression().Accept(new ExpressionVisitor(VisitingContext)),
				GetLocationFromToken(context.expression().Start));
			
			FunctionCallExpression latestFilterFunctionCall = null;

			foreach (var filter in context.filterChain().functionCallExpression())
			{
				latestFilterFunctionCall = filter.Accept(new FunctionCallExpressionVisitor(VisitingContext, implicitlyPassedArgument));
				implicitlyPassedArgument = new ArgumentValue(latestFilterFunctionCall, latestFilterFunctionCall.Location);
			}
			
			return latestFilterFunctionCall;
		}
	}
}
