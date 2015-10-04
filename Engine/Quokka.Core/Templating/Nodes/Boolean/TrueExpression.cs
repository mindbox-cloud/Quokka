namespace Quokka
{
	internal class TrueExpression : BooleanExpressionBase
	{
		public override bool Evaluate()
		{
			return true;
		}
	}
}
