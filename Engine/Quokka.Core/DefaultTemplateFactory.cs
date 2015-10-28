using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Quokka
{
	public class DefaultTemplateFactory : ITemplateFactory
	{
		private readonly FunctionRegistry functionRegistry;

		public DefaultTemplateFactory(IEnumerable<TemplateFunction> additionalFunctions = null)
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

		public HtmlTemplate CreateHtmlTemplate(string templateText)
		{
			return new HtmlTemplate(templateText, functionRegistry, true);
		}

		public HtmlTemplate TryCreateHtmlTemplate(string templateText, out IList<ITemplateError> errors)
		{
			var template = new HtmlTemplate(templateText, functionRegistry, false);
			errors = template.Errors;

			return errors.Any() ? null : template;
		}

		public ICompositeModelDefinition TryCombineModelDefinition(
			IEnumerable<ICompositeModelDefinition> definitions,
			out IList<ITemplateError> errors)
		{
			return ModelDefinitionTools.TryCombineModelDefinitions(definitions, out errors);
		}

		public ICompositeModelDefinition CombineModelDefinition(IEnumerable<ICompositeModelDefinition> definitions)
		{
			return ModelDefinitionTools.CombineModelDefinitions(definitions);
		}
	}
}
