using Quokka;

namespace Quokka
{
	internal abstract class ArithmeticExpressionBase : IArithmeticExpression
	{
		public abstract double GetValue(RenderContext renderContext);

		public abstract void CompileVariableDefinitions(SemanticAnalysisContext context);
	}
}
