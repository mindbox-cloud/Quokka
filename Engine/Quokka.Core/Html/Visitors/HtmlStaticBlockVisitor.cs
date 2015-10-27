using System;
using System.Collections.Generic;
using System.Linq;

using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

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
				context.children.Select(child => child.Accept(new StaticPartVisitor(visitingContext, context.Start.StartIndex)))
				.ToList();

			var blockText = context.Start.InputStream.GetText(
				new Interval(context.Start.StartIndex, context.Stop.StopIndex));
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

			return new StaticBlock(GrammarMergeTools.MergeInnerAndOuterBlocks(outerGrammarStaticChildren, staticSubBlocks));
		}
	}
}
