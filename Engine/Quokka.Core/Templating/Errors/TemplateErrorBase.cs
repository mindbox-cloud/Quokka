namespace Quokka
{
	internal abstract class TemplateErrorBase : ITemplateError
	{
		public string Message { get; }
		public int Line { get; }
		public int Column { get; }

		protected TemplateErrorBase(string message, int line, int column)
		{
			Message = message;
			Line = line;
			Column = column;
		}
	}
}
