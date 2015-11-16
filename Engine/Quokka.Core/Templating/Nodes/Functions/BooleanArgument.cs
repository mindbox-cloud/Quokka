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

		public override object GetValue(RenderContext renderContext)
		{
			return expression.Evaluate(renderContext);
		}
	}
}
