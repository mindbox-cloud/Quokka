namespace Quokka
{
	internal abstract class BooleanExpressionBase : IBooleanExpression
	{
		public abstract bool Evaluate(RenderContext renderContext);

		public abstract void CompileVariableDefinitions(SemanticAnalysisContext context);
	}
}
