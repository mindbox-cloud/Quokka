using System;
using System.Linq;

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
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

			// OR expression containing a single operand is logically equivalent to the operand itself.
			// We use this fact to "flatten" the expression tree.

			return andExpressions.Count > 1
				? new OrExpression(andExpressions)
				: andExpressions.Single();
		}

		public override IBooleanExpression VisitAndExpression(QuokkaParser.AndExpressionContext context)
		{
			var atoms = context.booleanAtom().Select(Visit).ToList();

			// AND expression containing a single operand is logically equivalent to the operand itself.
			// We use this fact to "flatten" the expression tree.

			return atoms.Count > 1
				? new AndExpression(atoms)
				: atoms.Single();
		}

		public override IBooleanExpression VisitNotExpression(QuokkaParser.NotExpressionContext context)
		{
			return new NotExpression(context.booleanAtom().Accept(this));
		}

		public override IBooleanExpression VisitStringComparisonExpression(QuokkaParser.StringComparisonExpressionContext context)
		{
			ComparisonOperation operation;

			if (context.Equals() != null)
				operation = ComparisonOperation.Equals;
			else if (context.NotEquals() != null)
				operation = ComparisonOperation.NotEquals;
			else
				throw new InvalidOperationException(
					"None of possible comparison operators encountered, the grammar is most likely faulty");

			return new StringComparisonExpression(
				context.variantValueExpression().Accept(new VariantValueExpressionVisitor(VisitingContext)),
				context.stringExpression().Accept(new StringExpressionVisitor(VisitingContext)),
				operation);
		}

		public override IBooleanExpression VisitNullComparisonExpression(QuokkaParser.NullComparisonExpressionContext context)
		{
			ComparisonOperation operation;

			if (context.Equals() != null)
				operation = ComparisonOperation.Equals;
			else if (context.NotEquals() != null)
				operation = ComparisonOperation.NotEquals;
			else
				throw new InvalidOperationException(
					"None of possible comparison operators encountered, the grammar is most likely faulty");

			return new NullComparisonExpression(
				context.variantValueExpression().Accept(new VariantValueExpressionVisitor(VisitingContext)),
				operation);
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

			var arithmeticVisitor = new ArithmeticExpressionVisitor(VisitingContext);
			return new ArithmeticComparisonExpression(
				operation,
				context.arithmeticExpression(0).Accept(arithmeticVisitor),
				context.arithmeticExpression(1).Accept(arithmeticVisitor));
		}

		public override IBooleanExpression VisitVariantValueExpression(QuokkaParser.VariantValueExpressionContext context)
		{
			return new VariantValueBooleanExpression(context.Accept(new VariantValueExpressionVisitor(VisitingContext)));
		}

		protected override IBooleanExpression AggregateResult(IBooleanExpression aggregate, IBooleanExpression nextResult)
		{
			// Works for Atom alternatives: we'll take the first alternative that is present.
			return aggregate ?? nextResult;
		}
	}
}
