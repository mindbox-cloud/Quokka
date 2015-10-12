namespace Quokka
{
	internal abstract class BooleanExpressionBase : IBooleanExpression
	{
		public abstract bool Evaluate(RenderContext renderContext);

		public virtual void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
		}
	}
}
