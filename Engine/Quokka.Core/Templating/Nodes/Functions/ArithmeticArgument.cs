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

		public override object GetValue(RenderContext renderContext)
		{
			return expression.GetValue(renderContext);
		}
	}
}
