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
