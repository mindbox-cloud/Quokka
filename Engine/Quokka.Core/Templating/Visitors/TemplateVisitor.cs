using System.Collections.Generic;
using System.Linq;
using Quokka.Generated;

namespace Quokka
{
	internal class TemplateVisitor : QuokkaBaseVisitor<ITemplateNode>
	{
		public static TemplateVisitor Instance { get; } = new TemplateVisitor();

		private TemplateVisitor()
		{
		}

		public override ITemplateNode VisitTemplateBlock(QuokkaParser.TemplateBlockContext context)
		{
			return new TemplateBlock(
				context.children
					.Select(child => child.Accept(this))
					.Where(x => x != null));
		}

		public override ITemplateNode VisitStaticBlock(QuokkaParser.StaticBlockContext context)
		{
			return new StaticBlock(context.children.Select(child => child.Accept(this)));
		}
		
		public override ITemplateNode VisitConstantBlock(QuokkaParser.ConstantBlockContext context)
		{
			return new ConstantBlock(context.GetText());
		}

		public override ITemplateNode VisitOutputBlock(QuokkaParser.OutputBlockContext context)
		{
			return context.Accept(OutputVisitor.Instance);
		}
		
		public override ITemplateNode VisitIfStatement(QuokkaParser.IfStatementContext context)
		{
			var conditions = new List<ConditionBlock>
			{
				context.ifCondition().Accept(ConditionsVisitor.Instance)
			};
			conditions.AddRange(context.elseIfCondition()
				.Select(elseIf => elseIf.Accept(ConditionsVisitor.Instance)));
			if (context.elseCondition() != null)
				conditions.Add(context.elseCondition().Accept(ConditionsVisitor.Instance));

			return new IfBlock(conditions);
		}
		
		public override ITemplateNode VisitForStatement(QuokkaParser.ForStatementContext context)
		{
			var forInstruction = context.forInstruction();
			var collectionVariable = forInstruction.parameterValueExpression().Accept(new VariableVisitor(VariableType.Array));

			var iterationVariableIdentifier = forInstruction.iterationVariable().Identifier();

			return new ForBlock(
				context.templateBlock()?.Accept(this),
				collectionVariable,
				new VariableDeclaration(
					iterationVariableIdentifier.GetText(),
					GetLocationFromToken(iterationVariableIdentifier.Symbol),
                    VariableType.Unknown,
					null));
		}

		public override ITemplateNode VisitCommentBlock(QuokkaParser.CommentBlockContext context)
		{
			return null;
		}
	}
}
