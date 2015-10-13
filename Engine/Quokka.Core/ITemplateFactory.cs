﻿using System.Collections.Generic;

namespace Quokka
{
	public interface ITemplateFactory
	{
		Template CreateTemplate(string templateText);

		Template TryCreateTemplate(string templateText, out IList<ITemplateError> errors);
	}
}