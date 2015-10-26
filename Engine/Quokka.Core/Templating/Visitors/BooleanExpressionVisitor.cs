using System;
using System.Linq;

using Quokka.Generated;

namespace Quokka
{
	internal class BooleanExpressionVisitor : QuokkaBaseVisitor<IBooleanExpression>
	{
		public BooleanExpressionVisitor(VisitingContext visitingContext)
			: base(visitingContext)
		{
		}

		public override IBooleanExpression VisitBooleanExpression(QuokkaParser.BooleanExpressionContext context)
		{
			var andExpressions = context.andExpression().Select(Visit).ToList();
			return andExpressions.Count > 1
				? new OrExpression(andExpressions)
				: andExpressions.Single();
		}

		public override IBooleanExpression VisitAndExpression(QuokkaParser.AndExpressionContext context)
		{
			var atoms = context.booleanAtom().Select(Visit).ToList();
			return atoms.Count > 1
				? new AndExpression(atoms)
				: atoms.Single();
		}

		public override IBooleanExpression VisitParameterValueExpression(QuokkaParser.ParameterValueExpressionContext context)
		{
			return new BooleanParameterValueExpression(
				context.Accept(new VariableVisitor(visitingContext, TypeDefinition.Boolean)));
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

			var arithmeticVisitor = new ArithmeticExpressionVisitor(visitingContext);
			return new ArithmeticComparisonExpression(
				operation,
				context.arithmeticExpression(0).Accept(arithmeticVisitor),
				context.arithmeticExpression(1).Accept(arithmeticVisitor));
		}
		
		public override IBooleanExpression VisitNotExpression(QuokkaParser.NotExpressionContext context)
		{
			return new NotExpression(Visit(context.booleanAtom()));
		}

		protected override IBooleanExpression AggregateResult(IBooleanExpression aggregate, IBooleanExpression nextResult)
		{
			// Works for Atom alternatives: we'll take the first alternative that is present.
			return aggregate ?? nextResult;
		}
	}
}
