using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quokka
{
	internal class TemplateBlock : TemplateNodeBase
	{
		private readonly IReadOnlyCollection<ITemplateNode> children;

		public TemplateBlock(IEnumerable<ITemplateNode> children)
		{
			this.children = children
				.ToList()
				.AsReadOnly();
		}

		public static TemplateBlock Empty()
		{
			return new TemplateBlock(Enumerable.Empty<ITemplateNode>());
		}

		public override void CompileVariableDefinitions(CompilationVariableScope scope)
		{
			foreach (var child in children)
				child.CompileVariableDefinitions(scope);
		}

		public override void Render(StringBuilder resultBuilder, RuntimeVariableScope variableScope)
		{
			foreach (var child in children)
				child.Render(resultBuilder, variableScope);
		}
	}
}
