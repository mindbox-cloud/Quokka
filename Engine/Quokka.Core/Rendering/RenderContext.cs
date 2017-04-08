using System.Text;

namespace Mindbox.Quokka
{
	internal class RenderContext
	{
		public RuntimeVariableScope VariableScope { get; }
		public FunctionRegistry Functions { get; }

		public RenderContext(RuntimeVariableScope variableScope, FunctionRegistry functions)
		{
			VariableScope = variableScope;
			Functions = functions;
		}

		public virtual RenderContext CreateInnerContext(RuntimeVariableScope variableScope)
		{
			return new RenderContext(variableScope, Functions);
		}

		public virtual void OnRenderingEnd(StringBuilder resultBuilder)
		{
		}
	}
}
