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
					// There are no more inner grammar blocks that need to be "merged" into this outer grammar block
					// (either there are no inner grammer blocks at all, or the next inner grammar block starts
					// further in the template, outside of the current outer grammar block)
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
								GetSubstringOfCodePoints(constantOuterBlock.Text, outerBlockPosition - outerBlock.Offset),
								outerBlockPosition,
								triviaLength));
						}

						outerBlockPosition = outerBlockEnd + 1;
					}

					// There are inner grammar blocks that need to be "merged" into this outer grammar block.
					else
					{
						int currentInnerBlockEnd = currentInnerBlock.Offset + currentInnerBlock.Length - 1;
						
						if (currentInnerBlock.Offset >= outerBlockPosition)
						{
							// There is a constant portion of outer grammar block that precedes the inner grammar block.
							// This portion must be added to the result stream.
							if (currentInnerBlock.Offset > outerBlockPosition)
							{
								var constantOuterBlock = outerBlock as ConstantBlock;
								if (constantOuterBlock == null)
									throw new InvalidOperationException("Inner grammar block starts in a non-constant block");

								var triviaLength = currentInnerBlock.Offset - outerBlockPosition;
								var leadingTriviaBlock = new ConstantBlock(
									GetSubstringOfCodePoints(constantOuterBlock.Text, outerBlockPosition - outerBlock.Offset, triviaLength),
									outerBlockPosition,
									triviaLength);
								result.Add(leadingTriviaBlock);
							}

							result.Add(currentInnerBlock);
						}

						// Current inner grammar block is zero length and precedes outer grammar block
						if (currentInnerBlockEnd + 1 == outerBlockPosition)
						{
							var nextInnerBlockIndex = currentInnerBlockIndex + 1;
							var nextInnerBlock = innerGrammarBlocks.Count > nextInnerBlockIndex
													? innerGrammarBlocks[nextInnerBlockIndex]
													: null;
							currentInnerBlockIndex = nextInnerBlockIndex;
							currentInnerBlock = nextInnerBlock;
						}
						else
						{
							// Current inner grammar block is fully contained within current outer grammar block.
							if (currentInnerBlockEnd <= outerBlockEnd)
							{
								outerBlockPosition = currentInnerBlockEnd + 1;

								var nextInnerBlockIndex = currentInnerBlockIndex + 1;
								var nextInnerBlock = innerGrammarBlocks.Count > nextInnerBlockIndex //start
														? innerGrammarBlocks[nextInnerBlockIndex]
														: null;

								// There is a constant portion of outer grammar block that immediately follows the inner grammar block.
								// This portion must be added to the result stream.
								if (currentInnerBlockEnd < outerBlockEnd)
								{
									var constantOuterBlock = outerBlock as ConstantBlock;
									if (constantOuterBlock == null)
										throw new InvalidOperationException("Inner grammar block ends in a non-constant block");

									var triviaLength = nextInnerBlock == null || nextInnerBlock.Offset > outerBlockEnd
															? outerBlock.Length - outerBlockPosition + outerBlock.Offset
															: nextInnerBlock.Offset - outerBlockPosition;

									var trailingTriviaBlock = new ConstantBlock(
										GetSubstringOfCodePoints(
											constantOuterBlock.Text,
											outerBlockPosition - outerBlock.Offset,
											triviaLength),
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
			}

			return result;
		}

		private static string GetSubstringOfCodePoints(string str, int startIndex, int? length = null)
		{
			int charStartIndex = 0;

			int charIndex = 0;
			int codePointIndex = 0;

			while (charIndex < str.Length && (length == null || codePointIndex - startIndex < length))
			{
				if (codePointIndex == startIndex)
					charStartIndex = charIndex;

				var character = str[charIndex];

				charIndex += char.IsHighSurrogate(character) ? 2 : 1;
				codePointIndex++;
			}

			return str.Substring(charStartIndex, charIndex - charStartIndex);
		}
	}
}
