namespace Mindbox.Quokka
{
	internal interface IBooleanExpression
	{
		bool Evaluate(RenderContext renderContext);

		void CompileVariableDefinitions(SemanticAnalysisContext context);
	}
}
