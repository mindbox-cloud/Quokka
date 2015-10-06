using System;

namespace Quokka
{
	internal class ArithmeticComparisonExpression : BooleanExpressionBase
	{
		public override bool Evaluate(VariableValueStorage valueStorage)
		{
			throw new NotImplementedException("Arithmetic comparison is not supported in conditions");
		}
	}
}
