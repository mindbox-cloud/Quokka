using Quokka.Generated;

namespace Quokka
{
	internal class OutputVisitor : QuokkaBaseVisitor<IOutputBlock>
	{
		public static OutputVisitor Instance { get; } = new OutputVisitor();

		private OutputVisitor()
		{
		}

		public override IOutputBlock VisitOutputBlock(QuokkaParser.OutputBlockContext context)
		{
			if (context.filterChain() != null)
				return new FunctionCallOutputBlock(context.Accept(FilterChainVisitor.Instance));

			return Visit(context.expression());
		}

		public override IOutputBlock VisitParameterValueExpression(QuokkaParser.ParameterValueExpressionContext context)
		{
			return new VariableOutputBlock(context.Accept(new VariableVisitor(VariableType.Primitive)));
		}

		public override IOutputBlock VisitFunctionCall(QuokkaParser.FunctionCallContext context)
		{
			return new FunctionCallOutputBlock(context.Accept(new FunctionCallVisitor()));
		}

		public override IOutputBlock VisitArithmeticExpression(QuokkaParser.ArithmeticExpressionContext context)
		{
			return new ArithmeticExpressionOutputBlock(context.Accept(ArithmeticExpressionVisitor.Instance));
		}

		public override IOutputBlock VisitBooleanExpression(QuokkaParser.BooleanExpressionContext context)
		{
			return new BooleanExpressionOutputBlock(context.Accept(BooleanExpressionVisitor.Instance));
		}

		public override IOutputBlock VisitStringConstant(QuokkaParser.StringConstantContext context)
		{
			return new StringConstantOutputBlock(context.Accept(StringConstantVisitor.Instance));
		}
	}
}
