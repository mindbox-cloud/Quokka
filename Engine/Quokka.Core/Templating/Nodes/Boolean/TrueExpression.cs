namespace Quokka
{
	internal class TrueExpression : BooleanExpressionBase
	{
		public override bool Evaluate(VariableValueStorage valueStorage)
		{
			return true;
		}
	}
}
