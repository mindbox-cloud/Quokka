namespace Quokka
{
	internal class ArithmeticExpressionOutputBlock : TemplateNodeBase
	{
		private readonly IArithmeticExpression expression;

		public ArithmeticExpressionOutputBlock(IArithmeticExpression expression)
		{
			this.expression = expression;
		}
	}
}
