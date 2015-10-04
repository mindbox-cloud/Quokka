namespace Quokka
{
	internal class ArithmeticExpressionOutputBlock : TemplateNodeBase
	{
		private readonly IArithmeticExpression expression;

		public ArithmeticExpressionOutputBlock(IArithmeticExpression expression)
		{
			this.expression = expression;
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			expression.CompileVariableDefinitions(scope, errorListener);
		}
	}
}
