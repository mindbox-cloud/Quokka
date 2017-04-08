using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class FilterChainVisitor : QuokkaBaseVisitor<FunctionCall>
	{
		public FilterChainVisitor(VisitingContext visitingContext)
			: base(visitingContext)
		{
		}

		public override FunctionCall VisitOutputBlock(QuokkaParser.OutputBlockContext context)
		{
			FunctionCall latestFilterFunctionCall = null;
			var implicitlyPassedArgument = context.expression().Accept(new FunctionArgumentVisitor(visitingContext));

			foreach (var filter in context.filterChain().functionCall())
			{
				latestFilterFunctionCall = filter.Accept(new FunctionCallVisitor(visitingContext, implicitlyPassedArgument));
				implicitlyPassedArgument = new FunctionCallArgument(latestFilterFunctionCall, latestFilterFunctionCall.Location);
			}

			return latestFilterFunctionCall;
		}
	}
}
