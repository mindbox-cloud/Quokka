using Quokka.Generated;

namespace Quokka
{
	internal class ConditionsVisitor : QuokkaBaseVisitor<ConditionBlock>
	{
		public static ConditionsVisitor Instance { get; } = new ConditionsVisitor();

		private ConditionsVisitor()
		{
		}

		public override ConditionBlock VisitIfCondition(QuokkaParser.IfConditionContext context)
		{
			return new ConditionBlock(
				context.ifInstruction().booleanExpression().Accept(BooleanExpressionVisitor.Instance),
				context.templateBlock()?.Accept(TemplateVisitor.Instance));
		}

		public override ConditionBlock VisitElseIfCondition(QuokkaParser.ElseIfConditionContext context)
		{
			return new ConditionBlock(
				context.elseIfInstruction().booleanExpression().Accept(BooleanExpressionVisitor.Instance),
				context.templateBlock()?.Accept(TemplateVisitor.Instance));
		}

		public override ConditionBlock VisitElseCondition(QuokkaParser.ElseConditionContext context)
		{
			return new ConditionBlock(
				new TrueExpression(),
				context.templateBlock()?.Accept(TemplateVisitor.Instance));
		}
	}
}
