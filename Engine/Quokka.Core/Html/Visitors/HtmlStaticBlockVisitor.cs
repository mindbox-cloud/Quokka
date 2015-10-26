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

			return new StaticBlock(MergeInnerAndOuterBlocks(outerGrammarStaticChildren, staticSubBlocks));
		}

		List<IStaticBlockPart> MergeInnerAndOuterBlocks(
			List<IStaticBlockPart> outerGrammarBlocks,
			List<IStaticBlockPart> innerGrammarBlocks)
		{
			var result = new List<IStaticBlockPart>();

			var currentInnerBlockIndex = 0;
			var currentInnerBlock = innerGrammarBlocks.Count > 0
				? innerGrammarBlocks[0]
				: null;

			foreach (var outerBlock in outerGrammarBlocks)
			{
				int outerBlockEnd = outerBlock.Offset + outerBlock.Length - 1;
				int outerBlockStart = outerBlock.Offset;

				if (currentInnerBlock == null || currentInnerBlock.Offset > outerBlockEnd)
				{
					result.Add(outerBlock);
				}
				else
				{
					int currentInnerBlockEnd = currentInnerBlock.Offset + currentInnerBlock.Length - 1;

					if (currentInnerBlock.Offset >= outerBlock.Offset)
					{
						if (currentInnerBlock.Offset > outerBlock.Offset)
						{
							var constantOuterBlock = outerBlock as ConstantBlock;
							if (constantOuterBlock == null)
								throw new InvalidOperationException("Inner grammar block starts in a non-constant block");

							var triviaLength = currentInnerBlock.Offset - outerBlock.Offset;
							var leadingTriviaBlock = new ConstantBlock(
								constantOuterBlock.Text.Substring(0, triviaLength),
								constantOuterBlock.Offset,
								triviaLength);
							result.Add(leadingTriviaBlock);
						}
						result.Add(currentInnerBlock);
					}
					
					if (currentInnerBlockEnd <= outerBlockEnd)
					{
						var nextInnerBlockIndex = currentInnerBlockIndex + 1;
						var nextInnerBlock = innerGrammarBlocks.Count > currentInnerBlockIndex
							? innerGrammarBlocks[currentInnerBlockIndex]
							: null;

						if (currentInnerBlockEnd < outerBlockEnd)
						{
							var constantOuterBlock = outerBlock as ConstantBlock;
							if (constantOuterBlock == null)
								throw new InvalidOperationException("Inner grammar block ends in a non-constant block");

							var triviaOffset = currentInnerBlockEnd - outerBlock.Offset + 1;

							var triviaLength = nextInnerBlock == null || nextInnerBlock.Offset > outerBlockEnd
								? constantOuterBlock.Length - triviaOffset
								: nextInnerBlock.Offset - triviaOffset;

							var trailingTriviaBlock = new ConstantBlock(
								constantOuterBlock.Text.Substring(triviaOffset),
								triviaOffset,
								triviaLength);
							result.Add(trailingTriviaBlock);
						}

						currentInnerBlockIndex = nextInnerBlockIndex;
						currentInnerBlock = nextInnerBlock;
					}
				}
			}

			return result;
		}
	}
}
