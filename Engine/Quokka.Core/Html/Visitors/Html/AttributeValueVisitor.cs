using System.Collections.Generic;
using System.Linq;
using System.Net;

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
			return CreateAttributeValue(context, 1, 1);
		}

		public override AttributeValue VisitSingleQuotedValue(QuokkaHtml.SingleQuotedValueContext context)
		{
			return CreateAttributeValue(context, 1, 1);
		}

		public override AttributeValue VisitUnquotedValue(QuokkaHtml.UnquotedValueContext context)
		{
			return CreateAttributeValue(context, 0, 0);
		}

		private AttributeValue CreateAttributeValue(
			Antlr4.Runtime.ParserRuleContext ruleContext,
			int offsetLeft,
			int offsetRight)
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
				return new AttributeValue(blockChildren, decodedValue, offset, length);
			}
			else
			{
				return null;
			}
		}
	}
}
