using System.Text;

namespace Quokka
{
	internal abstract class TemplateNodeBase : ITemplateNode
	{
		public virtual bool IsConstant { get; } = false;

		public virtual void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
		}

		public abstract void Render(StringBuilder resultBuilder, RenderContext context);

		public virtual void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context)
		{
		}
	}
}
