using Quokka;

namespace Quokka
{
	internal abstract class ArithmeticExpressionBase : IArithmeticExpression
	{
		public abstract double GetValue(RenderContext renderContext);

		public virtual void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
		}
	}
}
