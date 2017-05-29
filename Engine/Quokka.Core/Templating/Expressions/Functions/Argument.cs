namespace Mindbox.Quokka
{
	internal class Argument
	{
		public IExpression Expression { get; }

		public Location Location { get; }

		public Argument(IExpression expression, Location location)
		{
			Expression = expression;
			Location = location;
		}

		public void PerformSemanticAnalysis(AnalysisContext context, TypeDefinition requiredArgumentType)
		{
			Expression.PerformSemanticAnalysis(context, requiredArgumentType);
		}

		public TypeDefinition GetStaticType(AnalysisContext context)
		{
			return Expression.GetResultType(context);
		}

		public VariableValueStorage GetValue(RenderContext renderContext)
		{
			return Expression.Evaluate(renderContext);
		}

		public VariableValueStorage TryGetStaticValue()
		{
			return Expression.TryGetStaticEvaluationResult();
		}
	}
}
