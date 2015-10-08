namespace Quokka
{
	internal class SemanticError : TemplateErrorBase
	{
		public SemanticError(string message, int line, int column)
			: base(message, line, column)
		{
		}
	}
}
