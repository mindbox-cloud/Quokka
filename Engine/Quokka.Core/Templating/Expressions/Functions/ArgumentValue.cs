namespace Mindbox.Quokka
{
	internal class ArgumentValue
	{
		public IExpression Expression { get; }

		public Location Location { get; }

		public ArgumentValue(IExpression expression, Location location)
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

		public VariableValueStorage GetValue(RenderContext renderContext, TemplateFunctionArgument templateFunctionArgument)
		{
			if (templateFunctionArgument.AllowsNull && Expression.CheckIfExpressionIsNull(renderContext))
				return new PrimitiveVariableValueStorage(new PrimitiveModelValue(null));
			return Expression.Evaluate(renderContext);
		}

		public VariableValueStorage? TryGetStaticValue()
		{
			return Expression.TryGetStaticEvaluationResult();
		}
	}
}
