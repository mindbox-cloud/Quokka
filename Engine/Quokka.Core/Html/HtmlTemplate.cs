using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

		public override string Render(ICompositeModelValue model, ICallContextContainer callContextContainer = null)
		{
			var effectiveCallContextContainer = callContextContainer ?? CallContextContainer.Empty;

			var stringBuilder = new StringBuilder();
			using (var stringWriter = new StringWriter(stringBuilder))
			{
				DoRender(
					stringWriter,
					model,
					(scope, functionRegistry) => new HtmlRenderContext(
						scope,
						functionRegistry,
						null,
						null,
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
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			var effectiveCallContextContainer = callContextContainer ?? CallContextContainer.Empty;

			var stringBuilder = new StringBuilder();
			using (var stringWriter = new StringWriter(stringBuilder))
			{
				DoRender(
					stringWriter,
					model,
					(scope, functionRegistry) => new HtmlRenderContext(
						scope,
						functionRegistry,
						redirectLinkProcessor,
						identificationCode,
						effectiveCallContextContainer));
			}

			return stringBuilder.ToString();
		}

		public override void Render(TextWriter textWriter, ICompositeModelValue model, ICallContextContainer callContextContainer = null)
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
						effectiveCallContextContainer));
		}

		public void Render(
			TextWriter textWriter,
			ICompositeModelValue model,
			Func<Guid, string, string> redirectLinkProcessor,
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
					effectiveCallContextContainer));
		}
	}
}