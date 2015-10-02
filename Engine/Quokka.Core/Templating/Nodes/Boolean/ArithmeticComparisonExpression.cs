using System;

namespace Quokka
{
	internal class ArithmeticComparisonExpression : IBooleanExpression
	{
		public bool Evaluate()
		{
			throw new NotImplementedException("Arithmetic comparison is not supported in conditions");
		}
	}
}
