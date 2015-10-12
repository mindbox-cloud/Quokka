namespace Quokka
{
	public interface ITemplateError
	{
		Location Location { get; }
		string Message { get; }
	}
}
