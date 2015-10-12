namespace Quokka
{
	internal interface IArithmeticExpression
	{
		double GetValue(RenderContext renderContext);

		void CompileVariableDefinitions(SemanticAnalysisContext context);
	}
}
