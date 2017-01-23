using System.Collections.Generic;
using System.Linq;
using System.Net;

using Quokka.Generated;

namespace Quokka.Html
{
	internal class AttributeValueVisitor : QuokkaHtmlBaseVisitor<AttributeValue>
	{
		public AttributeValueVisitor(HtmlBlockParsingContext parsingContext)
			: base(parsingContext)
		{
		}

		public override AttributeValue VisitDoubleQuotedValue(QuokkaHtml.DoubleQuotedValueContext context)
		{
			var partsVisitor = new AttributeValuePartsVisitor(parsingContext);
			var blockChildren = context.children
				.Select(child => child.Accept(partsVisitor))
				.Where(block => block != null)
				.ToList();

			if (blockChildren.Any())
			{
				var offset = context.OpeningDoubleQuotes().Symbol.StartIndex + 1;
				var length = context.ClosingDoubleQuotes().Symbol.StartIndex - offset;
				var quotedText = context.GetText();
				var stringValue = quotedText.Substring(1, quotedText.Length - 2);
				return CreateAttributeValue(stringValue, blockChildren, offset, length);
			}
			else
			{
				return null;
			}
		}

		public override AttributeValue VisitSingleQuotedValue(QuokkaHtml.SingleQuotedValueContext context)
		{
			var partsVisitor = new AttributeValuePartsVisitor(parsingContext);
			var blockChildren = context.children
				.Select(child => child.Accept(partsVisitor))
				.Where(block => block != null)
				.ToList();
			if (blockChildren.Any())
			{
				var offset = context.OpeningSingleQuotes().Symbol.StartIndex + 1;
				var length = context.ClosingSingleQuotes().Symbol.StartIndex - offset;
				var quotedText = context.GetText();
				var stringValue = quotedText.Substring(1, quotedText.Length - 2);
				return CreateAttributeValue(stringValue, blockChildren, offset, length);
			}
			else
			{
				return null;
			}
		}

		public override AttributeValue VisitUnquotedValue(QuokkaHtml.UnquotedValueContext context)
		{
			var offset = context.Start.StartIndex;
			var length = context.Stop.StopIndex - offset + 1;
			var stringValue = context.GetText();
			return CreateAttributeValue(
				stringValue,
				new [] { new ConstantBlock(stringValue, offset, length) },
				offset,
				length);
		}
		private static AttributeValue CreateAttributeValue(
			string stringValue,
			IReadOnlyList<ITemplateNode> blockChildren,
			int offset,
			int length)
		{
			var decodedValue = WebUtility.HtmlDecode(stringValue);
			return new AttributeValue(blockChildren, decodedValue, offset, length);
		}
	}
}
