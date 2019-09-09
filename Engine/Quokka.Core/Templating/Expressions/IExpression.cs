namespace Mindbox.Quokka
{
	internal interface IExpression
	{
		VariableValueStorage? TryGetStaticEvaluationResult();

		VariableValueStorage Evaluate(RenderContext renderContext);

		TypeDefinition GetResultType(AnalysisContext context);

		void PerformSemanticAnalysis(AnalysisContext context, TypeDefinition expectedExpressionType);

		void RegisterAssignmentToVariable(
			AnalysisContext context, 
			ValueUsageSummary destinationVariable);

		string GetOutputValue(RenderContext context);

		bool CheckIfExpressionIsNull(RenderContext renderContext);
	}
}
