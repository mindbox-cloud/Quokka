using System.Linq;
using Quokka.Generated;

namespace Quokka
{
	internal class FunctionCallVisitor : QuokkaBaseVisitor<FunctionCall>
	{
		public static FunctionCallVisitor Instance { get; } = new FunctionCallVisitor();

		private FunctionCallVisitor()
		{
		}

		public override FunctionCall VisitFunctionCall(QuokkaParser.FunctionCallContext context)
		{
			return new FunctionCall(
				context.Identifier().GetText(),
				GetLocationFromToken(context.Identifier().Symbol),
				context.functionArgumentList().functionArgumentValue()
					.Select(argument => argument.Accept(FunctionArgumentVisitor.Instance)));
		}
	}
}
