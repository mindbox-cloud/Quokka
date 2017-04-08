using System.IO;

using Antlr4.Runtime;

namespace Mindbox.Quokka.Html
{
	internal class HtmlSyntaxErrorListener : SyntaxErrorListener
	{
		public override void SyntaxError(
			TextWriter output,
			IRecognizer recognizer,
			IToken offendingSymbol,
			int line,
			int charPositionInLine,
			string msg,
			RecognitionException e)
		{
			Errors.Add(new SyntaxError("Некорректная HTML-разметка", new Location(line, charPositionInLine)));
		}
	}
}
