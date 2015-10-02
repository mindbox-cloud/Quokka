using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class MultiplicationExpression : IArithmeticExpression
	{
		private readonly IReadOnlyCollection<MultiplicationOperand> operands;

		public MultiplicationExpression(IEnumerable<MultiplicationOperand> operands)
		{
			this.operands = operands.ToList().AsReadOnly();
		}

		public double GetValue()
		{
			return operands
				.Aggregate(1.0, (current, operand) => operand.Calculate(current));
		}
	}
}
