using System.Collections.Generic;
using System.Linq;

using Quokka.Html;

namespace Quokka
{
	public class HtmlTemplate : Template
	{
		internal HtmlTemplate(
			string templateText,
			FunctionRegistry functionRegistry,
			bool throwIfErrorsEncountered = true)
			: base(
				  templateText,
				  functionRegistry,
				  throwIfErrorsEncountered,
				  context => new HtmlStaticBlockVisitor(context))
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
	}
}
