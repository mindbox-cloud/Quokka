namespace Mindbox.Quokka
{
	internal abstract class TemplateErrorBase : ITemplateError
	{
		public string Message { get; }
		public Location Location { get; }

		protected TemplateErrorBase(string message, Location location)
		{
			Message = message;
			Location = location;
		}
	}
}
