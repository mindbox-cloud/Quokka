namespace Quokka
{
	internal abstract class BooleanExpressionBase : IBooleanExpression
	{
		public abstract bool Evaluate(VariableValueStorage valueStorage);

		public virtual void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
		}
	}
}
