using System;
using System.Collections.Generic;
using System.Linq;
using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class FunctionCallExpressionVisitor : QuokkaBaseVisitor<FunctionCallExpression>
	{
		/// <summary>
		/// First function argument that is passed to the function implicitly (used when functions are invoked
		/// via filter chain).
		/// </summary>
		private readonly ArgumentValue implicitlyPassedArgumentValue;

		public FunctionCallExpressionVisitor(
			VisitingContext visitingContext,
			ArgumentValue implicitlyPassedArgumentValue = null)
			: base(visitingContext)
		{
			this.implicitlyPassedArgumentValue = implicitlyPassedArgumentValue;
		}

		public override FunctionCallExpression VisitFunctionCallExpression(QuokkaParser.FunctionCallExpressionContext context)
		{
			var functionNameToken = context.Identifier() ?? context.If();
			if (functionNameToken == null)
				throw new InvalidOperationException("No function name token found");

			var arguments = new List<ArgumentValue>();
			if (implicitlyPassedArgumentValue != null)
				arguments.Add(implicitlyPassedArgumentValue);

			arguments.AddRange(
				context.argumentList().Accept(new ArgumentListVisitor(VisitingContext)));

			return new FunctionCallExpression(
				functionNameToken.GetText(),
				arguments,
				GetLocationFromToken(functionNameToken.Symbol));
		}
	}
}
