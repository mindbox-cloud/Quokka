// // Copyright 2022 Mindbox Ltd
// //
// // Licensed under the Apache License, Version 2.0 (the "License");
// // you may not use this file except in compliance with the License.
// // You may obtain a copy of the License at
// //
// //     http://www.apache.org/licenses/LICENSE-2.0
// //
// // Unless required by applicable law or agreed to in writing, software
// // distributed under the License is distributed on an "AS IS" BASIS,
// // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// // See the License for the specific language governing permissions and
// // limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;

using Mindbox.Quokka.Html;

namespace Mindbox.Quokka
{
	public class DefaultTemplateFactory : ITemplateFactory
	{
		private readonly FunctionRegistry functionRegistry;

		private readonly IReadOnlyCollection<string> nonIdempotentMethodNames;

		public DefaultTemplateFactory(
			IEnumerable<TemplateFunction> additionalFunctions = null,
			IEnumerable<string> nonIdempotentMethodNames = null)
		{
			var functions = new List<TemplateFunction>(Template.GetStandardFunctions());
			if (additionalFunctions != null)
				functions.AddRange(additionalFunctions);
			
			functionRegistry = new FunctionRegistry(functions);
			this.nonIdempotentMethodNames = nonIdempotentMethodNames?.ToArray() ?? Array.Empty<string>();
		}

		public ITemplate CreateTemplate(string templateText)
		{
			return new Template(
				templateText,
				functionRegistry,
				true,
				nonIdempotentMethodNames: nonIdempotentMethodNames);
		}

		public ITemplate TryCreateTemplate(string templateText, out IList<ITemplateError> errors)
		{
			var template = new Template(
				templateText,
				functionRegistry,
				false,
				nonIdempotentMethodNames: nonIdempotentMethodNames);
			errors = template.Errors;

			return errors.Any() ? null : template;
		}

		public IHtmlTemplate CreateHtmlTemplate(string templateText)
		{
			return new HtmlTemplate(templateText, functionRegistry, true, nonIdempotentMethodNames);
		}

		public IHtmlTemplate TryCreateHtmlTemplate(string templateText, out IList<ITemplateError> errors)
		{
			var template = new HtmlTemplate(templateText, functionRegistry, false, nonIdempotentMethodNames);
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
