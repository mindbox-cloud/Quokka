using System.Text;

namespace Quokka
{
	internal interface ITemplateNode
	{
		void CompileVariableDefinitions(SemanticAnalysisContext context);

		void Render(StringBuilder resultBuilder, RenderContext context);

		/// <summary>
		/// Compile data that is specific to the language (html, plaintext) that is used outside of control instructions.
		/// </summary>
		void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context);
	}
}
