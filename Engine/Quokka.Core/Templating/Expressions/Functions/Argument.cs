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

		public void CompileVariableDefinitions(SemanticAnalysisContext context, TypeDefinition requiredArgumentType)
		{
			Expression.CompileVariableDefinitions(context, requiredArgumentType);
		}

		public TypeDefinition GetStaticType(SemanticAnalysisContext context)
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
