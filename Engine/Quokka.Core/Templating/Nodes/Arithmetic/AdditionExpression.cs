using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class AdditionExpression : IArithmeticExpression
	{
		private readonly IReadOnlyCollection<AdditionOperand> operands;

		public AdditionExpression(IEnumerable<AdditionOperand> operands)
		{
			this.operands = operands.ToList().AsReadOnly();
		}

		public double GetValue()
		{
			return operands
				.Aggregate(0.0, (current, operand) => operand.Calculate(current));
		}
	}
}
