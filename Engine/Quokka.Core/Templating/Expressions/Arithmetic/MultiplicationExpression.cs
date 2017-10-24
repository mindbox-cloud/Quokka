using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	internal class MultiplicationExpression : ArithmeticExpression
	{
		private readonly IReadOnlyCollection<MultiplicationOperand> operands;

		public override TypeDefinition GetResultType(AnalysisContext context)
		{
			return operands.All(op => op.Expression.GetResultType(context) == TypeDefinition.Integer)
						? TypeDefinition.Integer
						: TypeDefinition.Decimal;
		}

		public MultiplicationExpression(IEnumerable<MultiplicationOperand> operands)
		{
			this.operands = operands.ToList().AsReadOnly();
		}

		public override double GetValue(RenderContext renderContext)
		{
			return operands
				.Aggregate(1.0, (current, operand) => operand.Calculate(current, renderContext));
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			foreach (var operand in operands)
				operand.Expression.PerformSemanticAnalysis(context);
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			return operands.Any(operand => operand.Expression.CheckIfExpressionIsNull(renderContext));
		}
	}
}
