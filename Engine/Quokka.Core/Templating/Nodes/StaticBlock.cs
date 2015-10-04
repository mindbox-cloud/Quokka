using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quokka
{
	internal class StaticBlock : TemplateNodeBase
	{
		private readonly IReadOnlyCollection<ITemplateNode> children; 
		private readonly string unprocessedText;

		public StaticBlock(IEnumerable<ITemplateNode> children, string unprocessedText)
		{
			this.children = children.ToList().AsReadOnly();
			this.unprocessedText = unprocessedText;
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			foreach (var child in children)
				child.CompileVariableDefinitions(scope, errorListener);
		}
	}
}
