using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class OutputVisitor : QuokkaBaseVisitor<IOutputBlock>
	{
		public OutputVisitor(VisitingContext visitingContext)
			: base(visitingContext)
		{
		}

		public override IOutputBlock VisitOutputBlock(QuokkaParser.OutputBlockContext context)
		{
			return Visit(context.expression());
		}

		public override IOutputBlock VisitParameterValueExpression(QuokkaParser.ParameterValueExpressionContext context)
		{
			return new VariableOutputBlock(context.Accept(new VariableVisitor(visitingContext, TypeDefinition.Primitive)));
		}

		public override IOutputBlock VisitFunctionCall(QuokkaParser.FunctionCallContext context)
		{
			return new FunctionCallOutputBlock(context.Accept(new FunctionCallVisitor(visitingContext)));
		}

		public override IOutputBlock VisitArithmeticExpression(QuokkaParser.ArithmeticExpressionContext context)
		{
			return new ArithmeticExpressionOutputBlock(context.Accept(new ArithmeticExpressionVisitor(visitingContext)));
		}

		public override IOutputBlock VisitBooleanExpression(QuokkaParser.BooleanExpressionContext context)
		{
			return new BooleanExpressionOutputBlock(context.Accept(new BooleanExpressionVisitor(visitingContext)));
		}

		public override IOutputBlock VisitStringConstant(QuokkaParser.StringConstantContext context)
		{
			return new StringConstantOutputBlock(context.Accept(new StringConstantVisitor(visitingContext)));
		}
	}
}
