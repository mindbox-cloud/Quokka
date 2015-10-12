using System;
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
			var functionNameToken = context.Identifier() ?? context.If();
			if (functionNameToken == null)
				throw new InvalidOperationException("No function name token found");

			return new FunctionCall(
				functionNameToken.GetText(),
				GetLocationFromToken(functionNameToken.Symbol),
				context.functionArgumentList().functionArgumentValue()
					.Select(argument => argument.Accept(FunctionArgumentVisitor.Instance)));
		}
	}
}
