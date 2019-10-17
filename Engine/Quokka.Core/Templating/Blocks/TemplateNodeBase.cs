using System.IO;
using System.Text;

namespace Mindbox.Quokka
{
	internal abstract class TemplateNodeBase : ITemplateNode
	{
		public virtual bool IsConstant => false;

		public virtual void PerformSemanticAnalysis(AnalysisContext context)
		{
		}

		public abstract void Render(TextWriter resultWriter, RenderContext renderContext);

		public virtual void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context)
		{
		}
	}
}
