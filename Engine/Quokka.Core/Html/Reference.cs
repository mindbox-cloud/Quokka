
namespace Quokka.Html
{
	public class Reference
	{
		public string RedirectUrl { get; }
		public string Name { get; }
		public bool IsConstant { get; }

		public Reference(string redirectUrl, string name, bool isConstant)
		{
			RedirectUrl = redirectUrl;
			Name = name;
			IsConstant = isConstant;
		}
	}
}
