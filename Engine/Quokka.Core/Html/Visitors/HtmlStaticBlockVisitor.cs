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
				context.children.Select(child => child.Accept(new StaticPartVisitor(VisitingContext, context.Start.StartIndex)))
				.ToList();

			var blockText = context.Start.InputStream.GetText(
				new Interval(context.Start.StartIndex, context.Stop.StopIndex));
			var inputStream = new CodePointCharStream(blockText);
			var lexer = new QuokkaHtmlLex(inputStream);
			lexer.RemoveErrorListeners();
			var commonTokenStream = new CommonTokenStream(lexer);
			
			var parser = new QuokkaHtml(commonTokenStream);
			parser.RemoveErrorListeners();
			var htmlSyntaxErrorListener = new HtmlSyntaxErrorListener();
			parser.AddErrorListener(htmlSyntaxErrorListener);

			var htmlBlock = parser.htmlBlock();

			VisitingContext.ErrorListener.MoveErrorsFromSubGrammar(
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
