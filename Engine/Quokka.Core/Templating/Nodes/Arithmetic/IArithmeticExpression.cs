namespace Quokka
{
	internal interface IArithmeticExpression
	{
		TypeDefinition Type { get; }

		double GetValue(RenderContext renderContext);

		void CompileVariableDefinitions(SemanticAnalysisContext context);
	}
}
