namespace Quokka
{
	internal class TrueExpression : BooleanExpressionBase
	{
		public override bool Evaluate(RenderContext renderContext)
		{
			return true;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
		}
	}
}
