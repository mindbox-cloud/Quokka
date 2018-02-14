namespace Mindbox.Quokka
{
	internal abstract class BooleanExpression : Expression
	{
		public abstract bool GetBooleanValue(RenderContext renderContext);

		public override TypeDefinition GetResultType(AnalysisContext context)
		{
			return TypeDefinition.Boolean;
		}

		public override VariableValueStorage Evaluate(RenderContext renderContext)
		{
			return new PrimitiveVariableValueStorage(GetBooleanValue(renderContext));
		}

		public sealed override void PerformSemanticAnalysis(AnalysisContext context, TypeDefinition expectedExpressionType)
		{
			PerformSemanticAnalysis(context);
		}

		public abstract void PerformSemanticAnalysis(AnalysisContext context);

		public override VariableValueStorage TryGetStaticEvaluationResult()
		{
			return null;
		}

		public sealed override void RegisterAssignmentToVariable(
			AnalysisContext context,
			ValueUsageSummary destinationVariable)
		{
			// do nothing
		}
	}
}
