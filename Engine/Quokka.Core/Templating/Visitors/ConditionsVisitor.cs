using Quokka.Generated;

namespace Quokka
{
	internal class ConditionsVisitor : QuokkaBaseVisitor<ConditionBlock>
	{
		public override ConditionBlock VisitIfCondition(QuokkaParser.IfConditionContext context)
		{
			return new ConditionBlock(
				context.ifInstruction().booleanExpression().Accept(new BooleanExpresionVisitor()),
				context.templateBlock()?.Accept(new TemplateCompilationVisitor()));
		}

		public override ConditionBlock VisitElseIfCondition(QuokkaParser.ElseIfConditionContext context)
		{
			return new ConditionBlock(
				context.elseIfInstruction().booleanExpression().Accept(new BooleanExpresionVisitor()),
				context.templateBlock()?.Accept(new TemplateCompilationVisitor()));
		}

		public override ConditionBlock VisitElseCondition(QuokkaParser.ElseConditionContext context)
		{
			return new ConditionBlock(
				new TrueExpression(),
				context.templateBlock()?.Accept(new TemplateCompilationVisitor()));
		}
	}
}
