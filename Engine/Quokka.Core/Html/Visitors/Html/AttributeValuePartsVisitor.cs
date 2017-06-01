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
			return new ConstantBlock(decodedText, context.Start.StartIndex, text.Length);
		}

		public override ITemplateNode VisitInsideAttributeOutputBlock(QuokkaHtml.InsideAttributeOutputBlockContext context)
		{
			int offset = context.Start.StartIndex;
			return ParsingContext.PreparsedOutputBlockNodes[offset];
		}
	}
}
