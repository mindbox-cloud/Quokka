using System.Collections.Generic;
using System.Linq;

using Antlr4.Runtime;

using Quokka.Generated;

namespace Quokka.Html
{
	internal class HtmlStaticBlockVisitor : QuokkaBaseVisitor<StaticBlock>
	{
		public HtmlStaticBlockVisitor(VisitingContext visitingContext)
			: base(visitingContext)
		{
		}

		public override StaticBlock VisitStaticBlock(QuokkaParser.StaticBlockContext context)
		{
			var outerGrammarStaticChildren = 
				context.children.Select(child => child.Accept(new TemplateVisitor(visitingContext)))
				.ToList();

			var blockText = context.GetText();
			var inputStream = new AntlrInputStream(blockText);
			var commonTokenStream = new CommonTokenStream(new QuokkaHtmlLex(inputStream));

			var parser = new QuokkaHtml(commonTokenStream);
			parser.RemoveErrorListeners();
			parser.AddErrorListener(visitingContext.ErrorListener);

			var htmlBlock = parser.htmlBlock();

			var outputBlocks = outerGrammarStaticChildren
				.OfType<OutputInstructionBlock>()
				.ToDictionary(
					outpuInstruction => outpuInstruction.Offset);

			var htmlBlockVisitor = new HtmlBlockVisitor(new HtmlBlockParsingContext(outputBlocks));
			var staticSubBlocks = htmlBlock.children
				.Select(child => child.Accept(htmlBlockVisitor))
				.Where(x => x != null)
				.ToList();

			return new StaticBlock(staticSubBlocks);
		}
	}
}
