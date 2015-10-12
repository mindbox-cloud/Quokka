using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class MultiplicationExpression : ArithmeticExpressionBase
	{
		private readonly IReadOnlyCollection<MultiplicationOperand> operands;

		public MultiplicationExpression(IEnumerable<MultiplicationOperand> operands)
		{
			this.operands = operands.ToList().AsReadOnly();
		}

		public override double GetValue(RenderContext renderContext)
		{
			return operands
				.Aggregate(1.0, (current, operand) => operand.Calculate(current, renderContext));
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			foreach (var operand in operands)
				operand.Expression.CompileVariableDefinitions(context);
		}
	}
}
