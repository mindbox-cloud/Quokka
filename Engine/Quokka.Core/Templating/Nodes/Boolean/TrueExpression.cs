namespace Quokka
{
	internal class TrueExpression : BooleanExpressionBase
	{
		public override bool Evaluate(RuntimeVariableScope variableScope)
		{
			return true;
		}
	}
}
