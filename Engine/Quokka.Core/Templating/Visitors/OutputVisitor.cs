using System;

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
			return context.parameterValueExpression()?.Accept(this)
					?? context.functionCall()?.Accept(this)
					?? context.arithmeticExpression()?.Accept(this);
		}

		public override IOutputBlock VisitParameterValueExpression(QuokkaParser.ParameterValueExpressionContext context)
		{
			return new VariableOutputBlock(context.Accept(new VariableVisitor(VariableType.Primitive)));
		}

		public override IOutputBlock VisitFunctionCall(QuokkaParser.FunctionCallContext context)
		{
			throw new NotImplementedException("Function calls are not supported");
		}

		public override IOutputBlock VisitArithmeticExpression(QuokkaParser.ArithmeticExpressionContext context)
		{
			return new ArithmeticExpressionOutputBlock(context.Accept(ArithmeticExpressionVisitor.Instance));
		}

		public override IOutputBlock VisitFilterChain(QuokkaParser.FilterChainContext context)
		{
			throw new NotImplementedException("Filter chains are not supported");
		}

	}
}
