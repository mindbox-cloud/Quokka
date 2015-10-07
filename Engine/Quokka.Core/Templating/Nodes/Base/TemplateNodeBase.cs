using System.Text;

namespace Quokka
{
	internal abstract class TemplateNodeBase : ITemplateNode
	{
		public virtual void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
		}

		public abstract void Render(StringBuilder resultBuilder, RuntimeVariableScope variableScope);
	}
}
