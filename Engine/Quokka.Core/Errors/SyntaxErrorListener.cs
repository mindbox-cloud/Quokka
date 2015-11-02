using System.Collections.Generic;

using Antlr4.Runtime;

namespace Quokka
{
	internal class SyntaxErrorListener : BaseErrorListener
	{
		private readonly List<SyntaxError> errors = new List<SyntaxError>();

		public IReadOnlyCollection<SyntaxError> GetErrors()
		{
			return errors.AsReadOnly();
		}

		public override void SyntaxError(
			IRecognizer recognizer,
			IToken offendingSymbol,
			int line,
			int charPositionInLine,
			string msg,
			RecognitionException e)
		{
			errors.Add(new SyntaxError("Недопустимый символ", new Location(line, charPositionInLine)));
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

				errors.Add(new SyntaxError(error.Message, new Location(absoluteLine, absoluteColumn)));
			}
		}
	}
}
