using System.Text;

namespace Quokka
{
	internal interface ITemplateNode
	{
		void CompileVariableDefinitions(SemanticAnalysisContext context);

		void Render(StringBuilder resultBuilder, RenderContext context);
	}
}
