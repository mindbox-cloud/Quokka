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
using System.IO;
using System.Linq;
using System.Text;

using Mindbox.Quokka.Abstractions;

namespace Mindbox.Quokka.Html
{
	internal class HtmlTemplate : Template, IHtmlTemplate
	{
		internal HtmlTemplate(
			string templateText,
			FunctionRegistry functionRegistry,
			bool throwIfErrorsEncountered = true)
			: base(
				templateText,
				functionRegistry,
				throwIfErrorsEncountered,
				context => new HtmlStaticBlockVisitor(context),
				new[] { new HtmlSemanticErrorSubListener() })
		{
		}

		public HtmlTemplate(string templateText)
			: this(
				templateText,
				new FunctionRegistry(GetStandardFunctions()))
		{
		}

		/// <summary>
		/// Get a collection of external references (links) ordered from top to bottom.
		/// </summary>
		public IReadOnlyList<Reference> GetReferences()
		{
			if (Errors.Any())
				throw new TemplateContainsErrorsException(Errors);

			var htmlContext = new HtmlDataAnalysisContext();
			CompileGrammarSpecificData(htmlContext);

			return htmlContext.GetReferences();
		}

		public override string Render(ICompositeModelValue model, RenderSettings settings, ICallContextContainer callContextContainer = null)
		{
			var effectiveCallContextContainer = callContextContainer ?? CallContextContainer.Empty;

			var stringBuilder = new StringBuilder();
			using (var stringWriter = new StringWriter(stringBuilder, settings.CultureInfo))
			{
				DoRender(
					stringWriter,
					model,
					(scope, functionRegistry) => new HtmlRenderContext(
						scope,
						functionRegistry,
						null,
						null,
						settings,
						effectiveCallContextContainer));
			}

			return stringBuilder.ToString();
		}

		public string Render(
			ICompositeModelValue model,
			Func<Guid, string, string> redirectLinkProcessor,
			string identificationCode = null,
			ICallContextContainer callContextContainer = null)
		{
			return Render(model, redirectLinkProcessor, RenderSettings.Default, identificationCode, callContextContainer);
		}

		public string Render(
			ICompositeModelValue model,
			RenderSettings settings,
			Func<Guid, string, string> redirectLinkProcessor,
			string identificationCode,
			ICallContextContainer callContextContainer = null)
		{
			return Render(model, redirectLinkProcessor, settings, identificationCode, callContextContainer);
		}

		public string Render(
			ICompositeModelValue model,
			Func<Guid, string, string> redirectLinkProcessor,
			RenderSettings settings,
			string identificationCode = null,
			ICallContextContainer callContextContainer = null)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			var effectiveCallContextContainer = callContextContainer ?? CallContextContainer.Empty;

			var stringBuilder = new StringBuilder();
			using (var stringWriter = new StringWriter(stringBuilder, settings.CultureInfo))
			{
				DoRender(
					stringWriter,
					model,
					(scope, functionRegistry) => new HtmlRenderContext(
						scope,
						functionRegistry,
						redirectLinkProcessor,
						identificationCode,
						settings,
						effectiveCallContextContainer));
			}

			return stringBuilder.ToString();
		}

		public override void Render(TextWriter textWriter, ICompositeModelValue model, RenderSettings settings, ICallContextContainer callContextContainer = null)
		{
			var effectiveCallContextContainer = callContextContainer ?? CallContextContainer.Empty;

			DoRender(
				textWriter,
					model,
					(scope, functionRegistry) => new HtmlRenderContext(
						scope,
						functionRegistry,
						null,
						null,
						settings,
						effectiveCallContextContainer));
		}

		public void Render(
			TextWriter textWriter,
			ICompositeModelValue model,
			Func<Guid, string, string> redirectLinkProcessor,
			string identificationCode = null,
			ICallContextContainer callContextContainer = null)
		{
			Render(textWriter, model, redirectLinkProcessor, RenderSettings.Default, identificationCode, callContextContainer);
		}

		public void Render(
			TextWriter textWriter,
			ICompositeModelValue model,
			RenderSettings settings,
			Func<Guid, string, string> redirectLinkProcessor,
			string identificationCode = null,
			ICallContextContainer callContextContainer = null)
		{
			Render(textWriter, model, redirectLinkProcessor, settings, identificationCode, callContextContainer);
		}

		public void Render(
			TextWriter textWriter,
			ICompositeModelValue model,
			Func<Guid, string, string> redirectLinkProcessor,
			RenderSettings settings,
			string identificationCode = null,
			ICallContextContainer callContextContainer = null)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			var effectiveCallContextContainer = callContextContainer ?? CallContextContainer.Empty;

			DoRender(
				textWriter,
				model,
				(scope, functionRegistry) => new HtmlRenderContext(
					scope,
					functionRegistry,
					redirectLinkProcessor,
					identificationCode,
					settings,
					effectiveCallContextContainer));
		}
	}
}
