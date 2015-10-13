﻿using System;
using System.Collections.Generic;
using System.Linq;
using Quokka.Generated;

namespace Quokka
{
	internal class FunctionCallVisitor : QuokkaBaseVisitor<FunctionCall>
	{
		/// <summary>
		/// First function argument that is passed to the function implicitly (used when functions are invoked
		/// via filter chain).
		/// </summary>
		private readonly IFunctionArgument implicitlyPassedArgument;

		public FunctionCallVisitor(IFunctionArgument implicitlyPassedArgument = null)
		{
			this.implicitlyPassedArgument = implicitlyPassedArgument;
		}

		public override FunctionCall VisitFunctionCall(QuokkaParser.FunctionCallContext context)
		{
			var functionNameToken = context.Identifier() ?? context.If();
			if (functionNameToken == null)
				throw new InvalidOperationException("No function name token found");

			var arguments = new List<IFunctionArgument>();
			if (implicitlyPassedArgument != null)
				arguments.Add(implicitlyPassedArgument);

			arguments.AddRange(
				context
					.functionArgumentList()
					.expression()
					.Select(argument => argument.Accept(FunctionArgumentVisitor.Instance)));

			return new FunctionCall(
				functionNameToken.GetText(),
				GetLocationFromToken(functionNameToken.Symbol),
				arguments);
		}
	}
}