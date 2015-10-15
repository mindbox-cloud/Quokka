using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	public class TemplateFactory : ITemplateFactory
	{
		private readonly FunctionRegistry functionRegistry;
		private readonly IModelDefinitionFactory modelDefinitionFactory;

		public TemplateFactory(
			IEnumerable<TemplateFunction> additionalFunctions = null,
			IModelDefinitionFactory modelDefinitionFactory = null)
		{
			var functions = new List<TemplateFunction>(Template.GetStandardFunctions());
			if (additionalFunctions != null)
				functions.AddRange(additionalFunctions);

			this.modelDefinitionFactory = modelDefinitionFactory ?? new ModelDefinitionFactory();
			functionRegistry = new FunctionRegistry(functions);
		}

		public Template CreateTemplate(string templateText)
		{
			return new Template(templateText, functionRegistry, modelDefinitionFactory, true);
		}

		public Template TryCreateTemplate(string templateText, out IList<ITemplateError> errors)
		{
			var template = new Template(templateText, functionRegistry, modelDefinitionFactory, false);
			errors = template.Errors;

			return errors.Any() ? null : template;
		}
	}
}
