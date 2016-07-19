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

		public override IStaticBlockPart VisitClosingTag(QuokkaHtml.ClosingTagContext context)
		{
			string tagName = context.TAG_NAME().GetText();

			if (StringComparer.InvariantCultureIgnoreCase.Equals(tagName, "body"))
				return new IdentificationCodePlaceHolderBlock(context.LeftAngularBracket().Symbol.StartIndex);
			return null;
		}

		private IStaticBlockPart TryGetLinkNodeFromTagAttributes(IEnumerable<QuokkaHtml.AttributeContext> attributes)
		{
			var hrefAttributeValueVisitor = new AttributeValueVisitor(parsingContext);

			AttributeValue hrefValue = null;
			AttributeValue nameValue = null;

			foreach (var attribute in attributes)
			{
				var attributeName = attribute.TAG_NAME().GetText();
				if (attributeName.Equals("href", StringComparison.InvariantCultureIgnoreCase))
					hrefValue = attribute.attributeValue()?.Accept(hrefAttributeValueVisitor);
				if (attributeName.Equals("data-name", StringComparison.InvariantCultureIgnoreCase))
					nameValue = attribute.attributeValue()?.Accept(hrefAttributeValueVisitor);
			}

			return hrefValue != null
				? new LinkBlock(hrefValue, nameValue)
				: null;
		}
	}
}
