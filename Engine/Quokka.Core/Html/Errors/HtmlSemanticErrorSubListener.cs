using System;

namespace Mindbox.Quokka
{
	internal class HtmlSemanticErrorSubListener : SemanticErrorSubListenerBase
	{
		public void AddHrefAttributeMustBeQuotedError(Location location)
		{
			AddError(new SemanticError(
				$"You have to use quotes with href attribute",
				location));
		}
	}
}