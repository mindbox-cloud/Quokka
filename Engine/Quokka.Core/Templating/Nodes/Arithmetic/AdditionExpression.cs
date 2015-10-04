using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class AdditionExpression : ArithmeticExpressionBase
	{
		private readonly IReadOnlyCollection<AdditionOperand> operands;

		public AdditionExpression(IEnumerable<AdditionOperand> operands)
		{
			this.operands = operands.ToList().AsReadOnly();
		}

		public override double GetValue()
		{
			return operands
				.Aggregate(0.0, (current, operand) => operand.Calculate(current));
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			foreach (var operand in operands)
				operand.Expression.CompileVariableDefinitions(scope, errorListener);
		}
	}
}
