using Quokka.Generated;

namespace Quokka.Html
{
	internal class AttributeValuePartsVisitor : QuokkaHtmlBaseVisitor<ITemplateNode>
	{
		public AttributeValuePartsVisitor(HtmlBlockParsingContext parsingContext)
			: base(parsingContext)
		{
		}

		public override ITemplateNode VisitInsideAttributeConstant(QuokkaHtml.InsideAttributeConstantContext context)
		{
			return new ConstantBlock(context.GetText());
		}

		public override ITemplateNode VisitInsideAttributeOutputBlock(QuokkaHtml.InsideAttributeOutputBlockContext context)
		{
			int offset = context.Start.StartIndex;
			return parsingContext.PreparsedOutputBlockNodes[offset];
		}
	}
}
