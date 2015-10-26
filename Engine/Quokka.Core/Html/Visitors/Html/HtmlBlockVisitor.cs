using System;
using System.Collections.Generic;

using Quokka.Generated;

namespace Quokka.Html
{
	internal class HtmlBlockVisitor : QuokkaHtmlBaseVisitor<IStaticBlockPart>
	{
		public HtmlBlockVisitor(HtmlBlockParsingContext parsingContext)
			: base(parsingContext)
		{
		}

		public override IStaticBlockPart VisitOpeningTag(QuokkaHtml.OpeningTagContext context)
		{
			string tagName = context.TAG_NAME().GetText();
			var attributes = context.attribute();

			if (tagName.Equals("a", StringComparison.InvariantCultureIgnoreCase)
				|| tagName.Equals("area", StringComparison.InvariantCultureIgnoreCase))
			{
				return TryGetLinkNodeFromTagAttributes(attributes);
			}
			return null;
		}

		public override IStaticBlockPart VisitSelfClosingTag(QuokkaHtml.SelfClosingTagContext context)
		{
			string tagName = context.TAG_NAME().GetText();
			var attributes = context.attribute();

			if (StringComparer.InvariantCultureIgnoreCase.Equals(tagName, "area"))
				return TryGetLinkNodeFromTagAttributes(attributes);
			return null;
		}

		private IStaticBlockPart TryGetLinkNodeFromTagAttributes(IEnumerable<QuokkaHtml.AttributeContext> attributes)
		{
			var hrefAttributeValueVisitor = new HrefAttributeValueVisitor(parsingContext);
			foreach (var attribute in attributes)
			{
				var attributeName = attribute.TAG_NAME().GetText();
				if (attributeName.Equals("href", StringComparison.InvariantCultureIgnoreCase))
					return attribute.attributeValue()?.Accept(hrefAttributeValueVisitor);
			}

			return null;
		}
	}
}
