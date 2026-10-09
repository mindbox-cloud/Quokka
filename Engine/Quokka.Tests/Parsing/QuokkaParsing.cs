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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka.Tests
{
	internal enum TwoStageParsingOutcome
	{
		SllParseTreeEqualsLl,
		SllParseTreeDiffersFromLl,
		SllBailedOnValidTemplate,
		SllBailedOnInvalidTemplate
	}

	internal sealed record TwoStageParsingComparison(
		TwoStageParsingOutcome Outcome,
		string SllParseTree,
		string LlParseTree);

	internal static class QuokkaParsing
	{
		private static readonly ConcurrentQueue<string> sllParseTreeMismatches = new();

		public static IReadOnlyCollection<string> SllParseTreeMismatches => sllParseTreeMismatches;

		public static void VerifySllParseTreeEqualsLl(string templateText, string sllParseTree)
		{
			var (llParseTree, _) = ParseWithLl(templateText);
			if (llParseTree == sllParseTree)
				return;

			var mismatch = DescribeMismatch(templateText, sllParseTree, llParseTree);
			sllParseTreeMismatches.Enqueue(mismatch);
			throw new AssertFailedException(mismatch);
		}

		public static TwoStageParsingComparison Compare(string templateText)
		{
			var (llParseTree, llSyntaxErrorCount) = ParseWithLl(templateText);

			var sllParser = CreateParser(templateText);
			sllParser.Interpreter.PredictionMode = PredictionMode.SLL;
			sllParser.ErrorHandler = new BailErrorStrategy();
			try
			{
				var sllParseTree = sllParser.template().ToStringTree(sllParser);
				return new TwoStageParsingComparison(
					sllParseTree == llParseTree
						? TwoStageParsingOutcome.SllParseTreeEqualsLl
						: TwoStageParsingOutcome.SllParseTreeDiffersFromLl,
					sllParseTree,
					llParseTree);
			}
			catch (ParseCanceledException)
			{
				return new TwoStageParsingComparison(
					llSyntaxErrorCount == 0
						? TwoStageParsingOutcome.SllBailedOnValidTemplate
						: TwoStageParsingOutcome.SllBailedOnInvalidTemplate,
					null,
					llParseTree);
			}
		}

		public static string DescribeMismatch(string templateText, string sllParseTree, string llParseTree) =>
			$"SLL parse tree differs from LL for template:{Environment.NewLine}{templateText}{Environment.NewLine}" +
			$"SLL: {sllParseTree}{Environment.NewLine}LL:  {llParseTree}";

		private static (string ParseTree, int SyntaxErrorCount) ParseWithLl(string templateText)
		{
			var parser = CreateParser(templateText);
			var syntaxErrors = new SyntaxErrorCounter();
			parser.AddErrorListener(syntaxErrors);
			parser.Interpreter.PredictionMode = PredictionMode.LL;
			var parseTree = parser.template().ToStringTree(parser);
			return (parseTree, syntaxErrors.Count);
		}

		private static QuokkaParser CreateParser(string templateText)
		{
			var lexer = new QuokkaLex(new CodePointCharStream(templateText));
			lexer.RemoveErrorListeners();
			var parser = new QuokkaParser(new CommonTokenStream(lexer));
			parser.RemoveErrorListeners();
			return parser;
		}

		private sealed class SyntaxErrorCounter : BaseErrorListener
		{
			public int Count { get; private set; }

			public override void SyntaxError(
				TextWriter output,
				IRecognizer recognizer,
				IToken offendingSymbol,
				int line,
				int charPositionInLine,
				string msg,
				RecognitionException e) =>
				Count++;
		}
	}
}
