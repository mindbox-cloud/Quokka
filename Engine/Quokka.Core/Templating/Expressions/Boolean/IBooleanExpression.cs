namespace Mindbox.Quokka
{
	internal interface IBooleanExpression : IExpression
	{
		bool GetBooleanValue(RenderContext renderContext);

		void CompileVariableDefinitions(SemanticAnalysisContext context);
	}
}
