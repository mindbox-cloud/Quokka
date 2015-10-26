using Quokka.Generated;

namespace Quokka
{
	internal class FunctionArgumentVisitor : QuokkaBaseVisitor<IFunctionArgument>
	{
		public FunctionArgumentVisitor(VisitingContext visitingContext)
			: base(visitingContext)
		{
		}

		public override IFunctionArgument VisitStringConstant(QuokkaParser.StringConstantContext context)
		{
			return new StringArgument(
				context.Accept(new StringConstantVisitor(visitingContext)),
				GetLocationFromToken(context.Start));
		}

		public override IFunctionArgument VisitParameterValueExpression(QuokkaParser.ParameterValueExpressionContext context)
		{
			return new ParameterValueArgument(
				context.Accept(new VariableVisitor(visitingContext, TypeDefinition.Primitive)),
				GetLocationFromToken(context.Start));
		}

		public override IFunctionArgument VisitFunctionCall(QuokkaParser.FunctionCallContext context)
		{
			return new FunctionCallArgument(
				context.Accept(new FunctionCallVisitor(visitingContext)),
				GetLocationFromToken(context.Start));
		}

		public override IFunctionArgument VisitBooleanExpression(QuokkaParser.BooleanExpressionContext context)
		{
			return new BooleanArgument(
				context.Accept(new BooleanExpressionVisitor(visitingContext)),
				GetLocationFromToken(context.Start));
		}

		public override IFunctionArgument VisitArithmeticExpression(QuokkaParser.ArithmeticExpressionContext context)
		{
			return new ArithmeticArgument(
				context.Accept(new ArithmeticExpressionVisitor(visitingContext)),
				GetLocationFromToken(context.Start));
		}
	}
}
