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

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka.Html
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

			if (tagName.Equals("body", StringComparison.InvariantCultureIgnoreCase))
			{
				return new PreHeaderPlaceHolderBlock(context.LeftAngularBracket().Symbol.StartIndex);
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
			var hrefAttributeValueVisitor = new AttributeValueVisitor(ParsingContext);

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
