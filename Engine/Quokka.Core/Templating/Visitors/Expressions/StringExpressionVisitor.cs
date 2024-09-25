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
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class StringExpressionVisitor : QuokkaBaseVisitor<StringExpression>
	{
		public StringExpressionVisitor(VisitingContext visitingContext)
		: base(visitingContext)
		{
		}

		public override StringExpression VisitStringConstant(QuokkaParser.StringConstantContext context)
		{
			var doubleQuotedString = context.DoubleQuotedString()?.GetText();

			if (doubleQuotedString != null)
			{
				string stringValue = doubleQuotedString.Substring(1, doubleQuotedString.Length - 2);
				return new StringConstantExpression(stringValue, "double");
			}
			else
			{
				var singleQuotedString = context.SingleQuotedString().GetText();

				string stringValue = singleQuotedString.Substring(1, singleQuotedString.Length - 2);
				return new StringConstantExpression(stringValue, "single");
			}
		}

		public override StringExpression VisitStringConcatenation(QuokkaParser.StringConcatenationContext context)
		{
			var firstOperand =
				context.stringAtom().variantValueExpression()?.Accept(new ExpressionVisitor(VisitingContext))
					?? context.stringAtom().stringConstant().Accept(this);

			return new StringConcatenationExpression(
					firstOperand,
					context.expression().Accept(new ExpressionVisitor(VisitingContext))
				);
		}
	}
}
