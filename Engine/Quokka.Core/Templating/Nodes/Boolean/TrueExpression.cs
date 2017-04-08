namespace Mindbox.Quokka
{
	internal class TrueExpression : BooleanExpressionBase
	{
		public override bool Evaluate(RenderContext renderContext)
		{
			return true;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			// This node is constant and therefore can't affect semantic analysis context.
		}
	}
}
