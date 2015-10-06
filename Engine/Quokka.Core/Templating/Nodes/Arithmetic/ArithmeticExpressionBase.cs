using Quokka;

namespace Quokka
{
	internal abstract class ArithmeticExpressionBase : IArithmeticExpression
	{
		public abstract double GetValue(VariableValueStorage variableValueStorage);

		public virtual void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
		}
	}
}
