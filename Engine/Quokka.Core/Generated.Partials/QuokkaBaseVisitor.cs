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

using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace Mindbox.Quokka.Generated
{
	internal partial class QuokkaBaseVisitor<Result>
	{
		protected VisitingContext VisitingContext { get; }

		protected Location GetLocationFromToken(IToken token)
		{
			return new Location(token.Line, token.Column);
		}

		private const int MaxExpressionSourceLength = 100;

		protected ExpressionSource GetExpressionSource(ParserRuleContext context)
		{
			var lastToken = context.Stop;
			var text = lastToken != null && lastToken.StopIndex >= context.Start.StartIndex
				? context.Start.InputStream.GetText(new Interval(context.Start.StartIndex, lastToken.StopIndex))
				: null;

			if (text != null && text.Length > MaxExpressionSourceLength)
				text = text.Substring(0, MaxExpressionSourceLength) + "…";

			return new ExpressionSource(GetLocationFromToken(context.Start), text);
		}

		protected QuokkaBaseVisitor(VisitingContext visitingContext)
		{
			VisitingContext = visitingContext;
		}
	}
}

