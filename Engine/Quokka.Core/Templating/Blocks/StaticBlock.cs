using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mindbox.Quokka
{
	internal class StaticBlock : TemplateNodeBase
	{
		private readonly IReadOnlyCollection<IStaticBlockPart> children;

		public override bool IsConstant
		{
			get { return children.All(child => child.IsConstant); }
		}

		public StaticBlock(IEnumerable<IStaticBlockPart> children)
		{
			this.children = children.ToList().AsReadOnly();
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			foreach (var child in children)
				child.PerformSemanticAnalysis(context);
		}

		public override void Render(TextWriter resultWriter, RenderContext renderContext)
		{
			foreach (var child in children)
				child.Render(resultWriter, renderContext);
		}

		public override void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context)
		{
			foreach (var child in children)
				child.CompileGrammarSpecificData(context);
		}
	}
}
