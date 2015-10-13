using System.Collections.Generic;

using Antlr4.Runtime;

namespace Quokka
{
	internal class SyntaxErrorListener : BaseErrorListener
	{
		private readonly List<SyntaxError> errors = new List<SyntaxError>();

		public IReadOnlyCollection<ITemplateError> GetErrors()
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
			errors.Add(new SyntaxError(msg, new Location(line, charPositionInLine)));
		}
	}
}
