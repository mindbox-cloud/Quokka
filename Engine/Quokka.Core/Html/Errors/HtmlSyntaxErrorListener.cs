using Antlr4.Runtime;

namespace Quokka.Html
{
	internal class HtmlSyntaxErrorListener : SyntaxErrorListener
	{
		public override void SyntaxError(
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
