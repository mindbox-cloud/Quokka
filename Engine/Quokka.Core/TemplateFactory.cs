using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	public class TemplateFactory : ITemplateFactory
	{
		private readonly FunctionRegistry functionRegistry;

		public TemplateFactory(IEnumerable<TemplateFunction> additionalFunctions = null)
		{
			var functions = new List<TemplateFunction>(Template.GetStandardFunctions());
			if (additionalFunctions != null)
				functions.AddRange(additionalFunctions);
			
			functionRegistry = new FunctionRegistry(functions);
		}

		public Template CreateTemplate(string templateText)
		{
			return new Template(templateText, functionRegistry,  true);
		}

		public Template TryCreateTemplate(string templateText, out IList<ITemplateError> errors)
		{
			var template = new Template(templateText, functionRegistry, false);
			errors = template.Errors;

			return errors.Any() ? null : template;
		}
	}
}
