using Quokka.Generated;

namespace Quokka
{
	internal class FilterChainVisitor : QuokkaBaseVisitor<FunctionCall>
	{
		public static FilterChainVisitor Instance { get; } = new FilterChainVisitor();

		private FilterChainVisitor()
		{
		}

		public override FunctionCall VisitOutputBlock(QuokkaParser.OutputBlockContext context)
		{
			FunctionCall latestFilterFunctionCall = null;
			var implicitlyPassedArgument = context.expression().Accept(FunctionArgumentVisitor.Instance);

			foreach (var filter in context.filterChain().functionCall())
			{
				latestFilterFunctionCall = filter.Accept(new FunctionCallVisitor(implicitlyPassedArgument));
				implicitlyPassedArgument = new FunctionCallArgument(latestFilterFunctionCall, latestFilterFunctionCall.Location);
			}

			return latestFilterFunctionCall;
		}
	}
}
