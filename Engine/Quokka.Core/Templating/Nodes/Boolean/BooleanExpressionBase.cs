namespace Quokka
{
	internal abstract class BooleanExpressionBase : IBooleanExpression
	{
		public abstract bool Evaluate();

		public virtual void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
		}
	}
}
