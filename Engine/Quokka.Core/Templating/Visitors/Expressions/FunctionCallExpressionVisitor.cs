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
