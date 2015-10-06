using System;
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
			ComparisonOperation operation;

			// This could be done with a separate small visitor but it probably would be an overkill here.

			if (context.Equals() != null)
				operation = ComparisonOperation.Equals;
			else if (context.NotEquals() != null)
				operation = ComparisonOperation.NotEquals;
			else if (context.LessThan() != null)
				operation = ComparisonOperation.LessThan;
			else if (context.GreaterThan() != null)
				operation = ComparisonOperation.GreaterThan;
			else if (context.LessThanOrEquals() != null)
				operation = ComparisonOperation.LessThanOrEquals;
			else if (context.GreaterThanOrEquals() != null)
				operation = ComparisonOperation.GreaterThanOrEquals;
			else
				throw new InvalidOperationException(
					"None of possible comparison operators encountered, the grammar is most likely faulty");

			return new ArithmeticComparisonExpression(
				operation,
				context.arithmeticExpression(0).Accept(new ArithmeticExpressionVisitor()),
				context.arithmeticExpression(1).Accept(new ArithmeticExpressionVisitor()));
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
