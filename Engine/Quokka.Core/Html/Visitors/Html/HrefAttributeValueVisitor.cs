using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quokka.Generated;

namespace Quokka.Html
{
	internal class HrefAttributeValueVisitor : QuokkaHtmlBaseVisitor<LinkBlock>
	{
		public HrefAttributeValueVisitor(HtmlBlockParsingContext parsingContext)
			: base(parsingContext)
		{
		}

		public override LinkBlock VisitDoubleQuotedValue(QuokkaHtml.DoubleQuotedValueContext context)
		{
			var partsVisitor = new AttributeValuePartsVisitor(parsingContext);
			var blockChildren = context.children.Select(child => child.Accept(partsVisitor)).ToList();
			if (blockChildren.Any())
			{
				var offset = context.OpeningDoubleQuotes().Symbol.StartIndex + 1;
				var length = context.ClosingDoubleQuotes().Symbol.StartIndex - offset;
				return new LinkBlock(blockChildren, offset, length);
			}
			else
			{
				return null;
			}
		}

		public override LinkBlock VisitSingleQuotedValue(QuokkaHtml.SingleQuotedValueContext context)
		{
			var partsVisitor = new AttributeValuePartsVisitor(parsingContext);
			var blockChildren = context.children.Select(child => child.Accept(partsVisitor)).ToList();
			if (blockChildren.Any())
			{
				var offset = context.OpeningSingleQuotes().Symbol.StartIndex + 1;
				var length = context.ClosingSingleQuotes().Symbol.StartIndex - offset;
				return new LinkBlock(blockChildren, offset, length);
			}
			else
			{
				return null;
			}
		}
	}
}
