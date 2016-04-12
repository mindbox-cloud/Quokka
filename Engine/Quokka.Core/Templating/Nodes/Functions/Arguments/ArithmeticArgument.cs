namespace Quokka
{
	internal class ArithmeticArgument : FunctionArgumentBase
	{
		private readonly IArithmeticExpression expression;

		public ArithmeticArgument(IArithmeticExpression expression, Location location)
			: base(location)
		{
			this.expression = expression;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context, TypeDefinition requiredArgumentType)
		{
			expression.CompileVariableDefinitions(context);
		}

		public override TypeDefinition TryGetStaticType(SemanticAnalysisContext context)
		{
			return expression.Type;
		}

		public override VariableValueStorage GetValue(RenderContext renderContext)
		{
			return new PrimitiveVariableValueStorage(expression.GetValue(renderContext));
		}
	}
}
