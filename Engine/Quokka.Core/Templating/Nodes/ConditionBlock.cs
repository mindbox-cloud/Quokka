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
	}
}
