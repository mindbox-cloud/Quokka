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

		public override double GetValue()
		{
			return operands
				.Aggregate(1.0, (current, operand) => operand.Calculate(current));
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			foreach (var operand in operands)
				operand.Expression.CompileVariableDefinitions(scope, errorListener);
		}
	}
}
