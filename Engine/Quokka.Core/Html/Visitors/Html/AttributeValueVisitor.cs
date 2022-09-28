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

using System.Collections.Generic;
using System.Linq;
using System.Net;
using Antlr4.Runtime;
using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka.Html
{
	internal class AttributeValueVisitor : QuokkaHtmlBaseVisitor<AttributeValue>
	{
		public AttributeValueVisitor(HtmlBlockParsingContext parsingContext)
			: base(parsingContext)
		{
		}

		public override AttributeValue VisitDoubleQuotedValue(QuokkaHtml.DoubleQuotedValueContext context)
		{
			return CreateAttributeValue(context, 1, 1, true);
		}

		public override AttributeValue VisitSingleQuotedValue(QuokkaHtml.SingleQuotedValueContext context)
		{
			return CreateAttributeValue(context, 1, 1, true);
		}

		public override AttributeValue VisitUnquotedValue(QuokkaHtml.UnquotedValueContext context)
		{
			return CreateAttributeValue(context, 0, 0, false);
		}

		private AttributeValue CreateAttributeValue(
			ParserRuleContext ruleContext,
			int offsetLeft,
			int offsetRight,
			bool isQuoted)
		{
			var partsVisitor = new AttributeValuePartsVisitor(ParsingContext);
			var blockChildren = ruleContext.children
				.Select(child => child.Accept(partsVisitor))
				.Where(block => block != null)
				.ToList();

			if (blockChildren.Any())
			{
				var offset = ruleContext.Start.StartIndex + offsetLeft;
				var length = ruleContext.GetContextLength() - offsetLeft - offsetRight;
				string text = ruleContext.GetText();
				var quotedText = text.Substring(offsetLeft, text.Length - offsetRight - offsetLeft);
				var decodedValue = WebUtility.HtmlDecode(quotedText);

				return new AttributeValue(blockChildren, decodedValue, offset, length, isQuoted, GetStartLocation(ruleContext));
			}
			else
			{
				return null;
			}
		}

		private static Location GetStartLocation(ParserRuleContext context)
		{
			return new Location(context.Start.Line, context.Start.Column);
		}
	}
}
