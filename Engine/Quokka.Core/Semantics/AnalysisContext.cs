using System;

namespace Mindbox.Quokka
{
	internal class AnalysisContext
	{
		public CompilationVariableScope VariableScope { get; }
		public FunctionRegistry Functions { get; }
		public ISemanticErrorListener ErrorListener { get; }

		public AnalysisContext(
			CompilationVariableScope variableScope,
			FunctionRegistry functions,
			ISemanticErrorListener errorListener)
		{
			VariableScope = variableScope;
			Functions = functions;
			ErrorListener = errorListener;
		}

		public AnalysisContext CreateNestedScopeContext()
		{
			return new AnalysisContext(
				VariableScope.CreateChildScope(),
				Functions,
				ErrorListener);
		}
	}
}
