using System;
using System.Collections.Generic;

namespace Quokka
{
	[Serializable]
	public class TemplateContainsErrorsException : TemplateException
	{
		public IList<ITemplateError> Errors { get; } 

		public TemplateContainsErrorsException(IList<ITemplateError> errors)
			: base("Template contains errors")
		{
			Errors = errors;
		}
	}
}
