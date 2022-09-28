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

using System.Net;
using System.Web;

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka.Html
{
	internal class AttributeValuePartsVisitor : QuokkaHtmlBaseVisitor<ITemplateNode>
	{
		public AttributeValuePartsVisitor(HtmlBlockParsingContext parsingContext)
			: base(parsingContext)
		{
		}

		public override ITemplateNode VisitInsideAttributeConstant(QuokkaHtml.InsideAttributeConstantContext context)
		{
			var text = context.GetText();
			var decodedText = WebUtility.HtmlDecode(text);
			return new ConstantBlock(decodedText, context.Start.StartIndex, context.GetContextLength());
		}

		public override ITemplateNode VisitInsideAttributeOutputBlock(QuokkaHtml.InsideAttributeOutputBlockContext context)
		{
			int offset = context.Start.StartIndex;
			return ParsingContext.PreparsedOutputBlockNodes[offset];
		}
	}
}
