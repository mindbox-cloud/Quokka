using System.Linq;

using Quokka.Generated;

namespace Quokka
{
	internal class BooleanExpressionVisitor : QuokkaBaseVisitor<IBooleanExpression>
	{
		public override IBooleanExpression VisitBooleanExpression(QuokkaParser.BooleanExpressionContext context)
		{
			return new OrExpression(context.andExpression().Select(Visit));
		}

		public override IBooleanExpression VisitAndExpression(QuokkaParser.AndExpressionContext context)
		{
			return new AndExpression(context.booleanAtom().Select(Visit));
		}

		public override IBooleanExpression VisitParameterValueExpression(QuokkaParser.ParameterValueExpressionContext context)
		{
			return new BooleanParameterValueExpression(
				context.Accept(new VariableVisitor(VariableType.Boolean)));
		}

		public override IBooleanExpression VisitArithmeticComparisonExpression(QuokkaParser.ArithmeticComparisonExpressionContext context)
		{
			return new ArithmeticComparisonExpression();
		}
		
		public override IBooleanExpression VisitNotExpression(QuokkaParser.NotExpressionContext context)
		{
			return new NotExpression(Visit(context.booleanAtom()));
		}

		protected override IBooleanExpression AggregateResult(IBooleanExpression aggregate, IBooleanExpression nextResult)
		{
			return aggregate ?? nextResult;
		}
	}
}
