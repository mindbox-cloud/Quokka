﻿namespace Quokka
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
	}
}
