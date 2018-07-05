using System;

namespace Mindbox.Quokka
{
	internal class HtmlSemanticErrorSubListener : SemanticErrorSubListenerBase
	{
		public void AddHrefAttributeMustBeQuotedError(Location location)
		{
			AddError(new SemanticError(
				$"Вы должны использовать кавычки при использовании атрибута href",
				location));
		}
	}
}