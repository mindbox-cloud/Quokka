using System.Collections.Generic;
using System.Linq;
using Quokka.Generated;

namespace Quokka
{
	internal class TemplateVisitor : QuokkaBaseVisitor<ITemplateNode>
	{
		public TemplateVisitor(VisitingContext visitingContext)
			: base(visitingContext)
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
			return context.Accept(visitingContext.CreateStaticBlockVisitor());
		}
		
		public override ITemplateNode VisitIfStatement(QuokkaParser.IfStatementContext context)
		{
			var conditionsVisitor = new ConditionsVisitor(visitingContext);
			var conditions = new List<ConditionBlock>
			{
				context.ifCondition().Accept(conditionsVisitor)
			};
			conditions.AddRange(context.elseIfCondition()
				.Select(elseIf => elseIf.Accept(conditionsVisitor)));
			if (context.elseCondition() != null)
				conditions.Add(context.elseCondition().Accept(conditionsVisitor));

			return new IfBlock(conditions);
		}
		
		public override ITemplateNode VisitForStatement(QuokkaParser.ForStatementContext context)
		{
			var forInstruction = context.forInstruction();

			var iterationVariableIdentifier = forInstruction.iterationVariable().Identifier();

			return new ForBlock(
				context.templateBlock()?.Accept(this),
				new VariableDeclaration(
					iterationVariableIdentifier.GetText(),
					GetLocationFromToken(iterationVariableIdentifier.Symbol),
					TypeDefinition.Unknown,
					null),
				forInstruction.Accept(new EnumerableElementVisitor(visitingContext)));
		}

		public override ITemplateNode VisitCommentBlock(QuokkaParser.CommentBlockContext context)
		{
			return null;
		}
	}
}
