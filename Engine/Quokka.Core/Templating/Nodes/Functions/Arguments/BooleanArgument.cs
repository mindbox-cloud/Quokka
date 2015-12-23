namespace Quokka
{
	internal class BooleanArgument : FunctionArgumentBase
	{
		private readonly IBooleanExpression expression;

		public BooleanArgument(IBooleanExpression expression, Location location)
			: base(location)
		{
			this.expression = expression;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context, TypeDefinition requiredArgumentType)
		{
			expression.CompileVariableDefinitions(context);
		}

		public override VariableValueStorage GetValue(RenderContext renderContext)
		{
			return new PrimitiveVariableValueStorage(expression.Evaluate(renderContext));
		}
	}
}
