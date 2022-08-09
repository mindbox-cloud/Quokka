﻿using System.Collections.Generic;

using Mindbox.Quokka.Html;

namespace Mindbox.Quokka
{
	public interface ITemplateFactory
	{
		ITemplate CreateTemplate(string templateText);

		ITemplate TryCreateTemplate(string templateText, out IList<ITemplateError> errors);

		IHtmlTemplate CreateHtmlTemplate(string templateText);

		IHtmlTemplate TryCreateHtmlTemplate(string templateText, out IList<ITemplateError> errors);
	}
}