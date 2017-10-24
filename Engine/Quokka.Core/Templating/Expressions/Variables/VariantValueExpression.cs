namespace Mindbox.Quokka
{
	internal abstract class VariantValueExpression : Expression
	{
		/// <summary>
		/// Collect all the semantic information about iterating over the expression result. All the information about the type
		/// of iteration variable will be used to compile the type of collection that is the result of the expression. 
		/// </summary>
		public abstract void RegisterIterationOverExpressionResult(AnalysisContext context, ValueUsageSummary iterationVariable);

		public abstract IModelDefinition GetExpressionResultModelDefinition(AnalysisContext context);
		
		public sealed override VariableValueStorage TryGetStaticEvaluationResult()
		{
			return null;
		}

		public sealed override TypeDefinition GetResultType(AnalysisContext context)
		{
			return TypeDefinition.GetTypeDefinitionFromModelDefinition(GetExpressionResultModelDefinition(context));
		}
	}
}