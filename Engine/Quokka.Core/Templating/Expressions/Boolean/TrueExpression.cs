namespace Mindbox.Quokka
{
	internal class TrueExpression : BooleanExpression
	{
		public override bool GetBooleanValue(RenderContext renderContext)
		{
			return true;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			// This node is constant and therefore can't affect semantic analysis context.
		}
	}
}
