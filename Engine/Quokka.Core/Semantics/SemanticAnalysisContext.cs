namespace Mindbox.Quokka
{
	internal class SemanticAnalysisContext
	{
		public CompilationVariableScope VariableScope { get; }
		public FunctionRegistry Functions { get; }
		public ISemanticErrorListener ErrorListener { get; }

		public SemanticAnalysisContext(
			CompilationVariableScope variableScope,
			FunctionRegistry functions,
			ISemanticErrorListener errorListener)
		{
			VariableScope = variableScope;
			Functions = functions;
			ErrorListener = errorListener;
		}

		public SemanticAnalysisContext CloneForNestedScope()
		{
			return new SemanticAnalysisContext(
				VariableScope.CreateChildScope(),
				Functions,
				ErrorListener);
		}
	}
}
