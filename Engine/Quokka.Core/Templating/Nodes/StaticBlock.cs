using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quokka
{
	internal class StaticBlock : TemplateNodeBase
	{
		private readonly IReadOnlyCollection<ITemplateNode> children; 

		public StaticBlock(IEnumerable<ITemplateNode> children)
		{
			this.children = children.ToList().AsReadOnly();
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			foreach (var child in children)
				child.CompileVariableDefinitions(context);
		}

		public override void Render(StringBuilder resultBuilder, RenderContext context)
		{
			foreach (var child in children)
				child.Render(resultBuilder, context);
		}
	}
}
