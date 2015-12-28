using System;
using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	[Serializable]
	public class TemplateContainsErrorsException : TemplateException
	{
		public IList<ITemplateError> Errors { get; } 

		public TemplateContainsErrorsException(IList<ITemplateError> errors)
			: base("Template contains errors:" +
				  Environment.NewLine +
				  string.Join(Environment.NewLine, 
					 errors.Select(x => x.Message)))
		{
			Errors = errors;
		}
	}
}
