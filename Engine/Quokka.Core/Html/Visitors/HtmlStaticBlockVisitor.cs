using System;
using System.Collections.Generic;
using System.Linq;

using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka.Html
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
			var lexer = new QuokkaHtmlLex(inputStream);
			lexer.RemoveErrorListeners();
			var commonTokenStream = new CommonTokenStream(lexer);
			
			var parser = new QuokkaHtml(commonTokenStream);
			parser.RemoveErrorListeners();
			var htmlSyntaxErrorListener = new HtmlSyntaxErrorListener();
			parser.AddErrorListener(htmlSyntaxErrorListener);

			var htmlBlock = parser.htmlBlock();

			visitingContext.ErrorListener.MoveErrorsFromSubGrammar(
				htmlSyntaxErrorListener.GetErrors(),
				context.Start.Line - 1,
				context.Start.Column);

			var outputBlocks = outerGrammarStaticChildren
				.OfType<OutputInstructionBlock>()
				.ToDictionary(
					outpuInstruction => outpuInstruction.Offset);

			var htmlBlockVisitor = new HtmlBlockVisitor(new HtmlBlockParsingContext(outputBlocks));
			var staticSubBlocks = htmlBlock.children
				.Select(child => child.Accept(htmlBlockVisitor))
				.Where(x => x != null)
				.ToList();

			var mergedBlocks = GrammarMergeTools.MergeInnerAndOuterBlocks(outerGrammarStaticChildren, staticSubBlocks);
			return new StaticBlock(mergedBlocks);
		}
	}
}
