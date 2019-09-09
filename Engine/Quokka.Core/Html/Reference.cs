
using System;

namespace Mindbox.Quokka.Html
{
	public class Reference
	{
		public Guid UniqueKey { get; }
		public string RedirectUrl { get; }
		public string? Name { get; }
		public bool IsConstant { get; }

		public Reference(string redirectUrl, string? name, Guid uniqueKey, bool isConstant)
		{
			RedirectUrl = redirectUrl;
			Name = name;
			UniqueKey = uniqueKey;
			IsConstant = isConstant;
		}
	}
}
