namespace Quokka
{
	internal class ConditionBlock : TemplateNodeBase
	{
		private readonly IBooleanExpression condition;
		private readonly ITemplateNode block;
		
		public ConditionBlock(IBooleanExpression condition, ITemplateNode block)
		{
			this.block = block;
			this.condition = condition;
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			condition.CompileVariableDefinitions(scope, errorListener);
			block.CompileVariableDefinitions(scope, errorListener);
		}
	}
}
