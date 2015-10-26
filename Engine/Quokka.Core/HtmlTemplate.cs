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
	}
}
