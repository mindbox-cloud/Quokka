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
			
			FunctionCallExpression? latestFilterFunctionCall = null;

			foreach (var filter in context.filterChain().functionCallExpression())
			{
				latestFilterFunctionCall = filter.Accept(new FunctionCallExpressionVisitor(VisitingContext, implicitlyPassedArgument));
				implicitlyPassedArgument = new ArgumentValue(latestFilterFunctionCall, latestFilterFunctionCall.Location);
			}
			
			if (latestFilterFunctionCall == null)
				throw new InvalidOperationException("No function call found in the filter chain");

			return latestFilterFunctionCall;
		}
	}
}
