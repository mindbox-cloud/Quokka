using System.Text;

namespace Quokka
{
	internal abstract class TemplateNodeBase : ITemplateNode
	{
		public virtual void CompileVariableDefinitions(CompilationVariableScope scope)
		{
		}

		public abstract void Render(StringBuilder resultBuilder, RuntimeVariableScope variableScope);
	}
}
