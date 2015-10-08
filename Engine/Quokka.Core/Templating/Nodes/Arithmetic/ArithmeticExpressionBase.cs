using Quokka;

namespace Quokka
{
	internal abstract class ArithmeticExpressionBase : IArithmeticExpression
	{
		public abstract double GetValue(RuntimeVariableScope variableScope);

		public virtual void CompileVariableDefinitions(CompilationVariableScope scope)
		{
		}
	}
}
