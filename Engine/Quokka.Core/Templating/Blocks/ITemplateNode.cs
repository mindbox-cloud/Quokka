using System.Text;

namespace Mindbox.Quokka
{
	internal interface ITemplateNode
	{
		bool IsConstant { get; }

		void CompileVariableDefinitions(SemanticAnalysisContext context);

		void Render(StringBuilder resultBuilder, RenderContext renderContext);

		/// <summary>
		/// Compile data that is specific to the language (html, plaintext) that is used outside of control instructions.
		/// </summary>
		void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context);
	}
}
