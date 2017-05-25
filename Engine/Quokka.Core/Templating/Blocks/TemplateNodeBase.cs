using System.Text;

namespace Mindbox.Quokka
{
	internal abstract class TemplateNodeBase : ITemplateNode
	{
		public virtual bool IsConstant => false;

		public virtual void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
		}

		public abstract void Render(StringBuilder resultBuilder, RenderContext renderContext);

		public virtual void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context)
		{
		}
	}
}
