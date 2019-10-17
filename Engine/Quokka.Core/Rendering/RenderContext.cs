using System;
using System.IO;
using System.Text;

namespace Mindbox.Quokka
{
	internal class RenderContext
	{
		public RuntimeVariableScope VariableScope { get; }
		public FunctionRegistry Functions { get; }
		public CallContextContainer CallContextContainer { get; }

		public RenderContext(
			RuntimeVariableScope variableScope, FunctionRegistry functions, CallContextContainer callContextContainer)
		{
			if (callContextContainer == null) 
				throw new ArgumentNullException(nameof(callContextContainer));

			VariableScope = variableScope;
			Functions = functions;
			CallContextContainer = callContextContainer;
		}

		public virtual RenderContext CreateInnerContext(RuntimeVariableScope variableScope)
		{
			return new RenderContext(variableScope, Functions, CallContextContainer);
		}

		public virtual void OnRenderingEnd(TextWriter resultWriter)
		{
		}

		public TContext GetCallContextValue<TContext>()
			where TContext : class
		{
			return CallContextContainer.GetCallContext<TContext>();
		}
	}
}
