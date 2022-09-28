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

using System.Collections.Generic;
using System.IO;

using Antlr4.Runtime;

namespace Mindbox.Quokka
{
	internal class SyntaxErrorListener : BaseErrorListener
	{
		protected List<SyntaxError> Errors { get; } = new List<SyntaxError>();

		public IReadOnlyCollection<SyntaxError> GetErrors()
		{
			return Errors.AsReadOnly();
		}

		public override void SyntaxError(
			TextWriter output,
			IRecognizer recognizer,
			IToken offendingSymbol,
			int line,
			int charPositionInLine,
			string msg,
			RecognitionException e)
		{
			Errors.Add(new SyntaxError("Invalid symbol", new Location(line, charPositionInLine)));
		}

		/// <summary>
		/// Add syntax error from inner grammar (parsed separately). These errors will have locations relative to the beginning
		/// of the inner grammar block, so we need to translate them to their absolute locations inside the source string.
		/// </summary>
		/// <param name="subgrammarErrors">Inner grammar errors</param>
		/// <param name="firstLineOffset">Zero-based offset of the first line of the subgrammar</param>
		/// <param name="firstLineColumnOffset">Zero-based offset of the first column of the first line of the subgrammar</param>
		public void MoveErrorsFromSubGrammar(
			IEnumerable<SyntaxError> subgrammarErrors,
			int firstLineOffset, 
			int firstLineColumnOffset)
		{
			foreach (var error in subgrammarErrors)
			{
				var absoluteLine = error.Location.Line + firstLineOffset;
				var absoluteColumn = error.Location.Line == 1
					? error.Location.Column + firstLineColumnOffset
					: error.Location.Column;

				Errors.Add(new SyntaxError(error.Message, new Location(absoluteLine, absoluteColumn)));
			}
		}
	}
}
