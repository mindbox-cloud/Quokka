namespace Mindbox.Quokka
{
	internal class SemanticError : TemplateErrorBase
	{
		public SemanticError(string message, Location? location)
			: base(message, location)
		{
		}
	}
}
