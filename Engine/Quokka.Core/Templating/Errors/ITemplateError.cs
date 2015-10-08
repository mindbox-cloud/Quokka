namespace Quokka
{
	public interface ITemplateError
	{
		int Line { get; }
		int Column { get; }
		string Message { get; }
	}
}
