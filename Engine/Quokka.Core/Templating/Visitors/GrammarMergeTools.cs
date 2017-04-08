using System;
using System.Collections.Generic;

namespace Mindbox.Quokka
{
	internal static class GrammarMergeTools
	{
		internal static List<IStaticBlockPart> MergeInnerAndOuterBlocks(
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
				int outerBlockPosition = outerBlock.Offset;

				while (outerBlockPosition <= outerBlockEnd)
				{
					if (currentInnerBlock == null || currentInnerBlock.Offset > outerBlockEnd)
					{
						if (outerBlockPosition == outerBlock.Offset)
						{
							result.Add(outerBlock);
						}
						else
						{
							var constantOuterBlock = outerBlock as ConstantBlock;
							if (constantOuterBlock == null)
								throw new InvalidOperationException("Trying to split a non-constant block");

							var triviaLength = outerBlockEnd - outerBlockPosition + 1;
							result.Add(new ConstantBlock(
								constantOuterBlock.Text.Substring(outerBlockPosition - outerBlock.Offset),
								outerBlockPosition,
								triviaLength));
						}

						outerBlockPosition = outerBlockEnd + 1;
					}
					else
					{
						int currentInnerBlockEnd = currentInnerBlock.Offset + currentInnerBlock.Length - 1;

						if (currentInnerBlock.Offset >= outerBlockPosition)
						{
							if (currentInnerBlock.Offset > outerBlockPosition)
							{
								var constantOuterBlock = outerBlock as ConstantBlock;
								if (constantOuterBlock == null)
									throw new InvalidOperationException("Inner grammar block starts in a non-constant block");

								var triviaLength = currentInnerBlock.Offset - outerBlockPosition;
								var leadingTriviaBlock = new ConstantBlock(
									constantOuterBlock.Text.Substring(outerBlockPosition - outerBlock.Offset, triviaLength),
									outerBlockPosition,
									triviaLength);
								result.Add(leadingTriviaBlock);
							}
							result.Add(currentInnerBlock);
						}

						if (currentInnerBlockEnd <= outerBlockEnd)
						{
							outerBlockPosition = currentInnerBlockEnd + 1;

							var nextInnerBlockIndex = currentInnerBlockIndex + 1;
							var nextInnerBlock = innerGrammarBlocks.Count > nextInnerBlockIndex
								? innerGrammarBlocks[nextInnerBlockIndex]
								: null;

							if (currentInnerBlockEnd < outerBlockEnd)
							{
								var constantOuterBlock = outerBlock as ConstantBlock;
								if (constantOuterBlock == null)
									throw new InvalidOperationException("Inner grammar block ends in a non-constant block");

								var triviaLength = nextInnerBlock == null || nextInnerBlock.Offset > outerBlockEnd
									? outerBlock.Length - outerBlockPosition + outerBlock.Offset
									: nextInnerBlock.Offset - outerBlockPosition;

								var trailingTriviaBlock = new ConstantBlock(
									constantOuterBlock.Text.Substring(outerBlockPosition - outerBlock.Offset, triviaLength),
									outerBlockPosition,
									triviaLength);
								result.Add(trailingTriviaBlock);

								outerBlockPosition += triviaLength;
							}

							currentInnerBlockIndex = nextInnerBlockIndex;
							currentInnerBlock = nextInnerBlock;
						}
						else
						{
							outerBlockPosition = outerBlockEnd + 1;
						}
					}
				}
			}

			return result;
		}
	}
}
