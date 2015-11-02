using Quokka.Generated;

namespace Quokka.Html
{
	internal class LinkNameAttributeValueVisitor : QuokkaHtmlBaseVisitor<string>
	{
		public LinkNameAttributeValueVisitor(HtmlBlockParsingContext parsingContext)
			: base(parsingContext)
		{
		}

		public override string VisitInsideAttributeConstant(QuokkaHtml.InsideAttributeConstantContext context)
		{
			return base.VisitInsideAttributeConstant(context);
		}

		public override string VisitInsideAttributeOutputBlock(QuokkaHtml.InsideAttributeOutputBlockContext context)
		{
			return base.VisitInsideAttributeOutputBlock(context);
		}
	}
}
