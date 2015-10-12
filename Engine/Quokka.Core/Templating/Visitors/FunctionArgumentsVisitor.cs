﻿using Quokka.Generated;

namespace Quokka
{
	internal class FunctionArgumentVisitor : QuokkaBaseVisitor<IFunctionArgument>
	{
		public static FunctionArgumentVisitor Instance { get; } = new FunctionArgumentVisitor();

		private FunctionArgumentVisitor()
		{
		}

		public override IFunctionArgument VisitStringConstant(QuokkaParser.StringConstantContext context)
		{
			var quotedString = context.DoubleQuotedString().GetText();
			return new StringArgument(
				quotedString.Substring(1, quotedString.Length - 2),
				GetLocationFromToken(context.Start));
		}

		public override IFunctionArgument VisitParameterValueExpression(QuokkaParser.ParameterValueExpressionContext context)
		{
			return new ParameterValueArgument(
				context.Accept(new VariableVisitor(VariableType.Primitive)),
				GetLocationFromToken(context.Start));
		}

		public override IFunctionArgument VisitFunctionCall(QuokkaParser.FunctionCallContext context)
		{
			return new FunctionCallArgument(
				context.Accept(FunctionCallVisitor.Instance),
				GetLocationFromToken(context.Start));
		}

		public override IFunctionArgument VisitBooleanExpression(QuokkaParser.BooleanExpressionContext context)
		{
			return new BooleanArgument(
				context.Accept(BooleanExpressionVisitor.Instance),
				GetLocationFromToken(context.Start));
		}

		public override IFunctionArgument VisitArithmeticExpression(QuokkaParser.ArithmeticExpressionContext context)
		{
			return new ArithmeticArgument(
				context.Accept(ArithmeticExpressionVisitor.Instance),
				GetLocationFromToken(context.Start));
		}
	}
}
