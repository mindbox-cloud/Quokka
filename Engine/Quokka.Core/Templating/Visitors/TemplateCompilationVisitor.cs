using System;
using System.Collections.Generic;
using System.Linq;

using Antlr4.Runtime.Tree;

using Quokka.Generated;

namespace Quokka
{
	internal class TemplateCompilationVisitor : QuokkaBaseVisitor<ITemplateNode>
	{
		public static TemplateCompilationVisitor Instance { get; } = new TemplateCompilationVisitor();

		private TemplateCompilationVisitor()
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
			return new StaticBlock(
				context.children.Select(child => child.Accept(this)));
		}
		
		public override ITemplateNode VisitConstantBlock(QuokkaParser.ConstantBlockContext context)
		{
			return new ConstantBlock(context.GetText());
		}

		public override ITemplateNode VisitOutputBlock(QuokkaParser.OutputBlockContext context)
		{
			var filteredParameterValue = context.filteredParameterValueExpression();
			if (filteredParameterValue != null)
				return Visit(filteredParameterValue);

			var arithmeticExpression = context.arithmeticExpression();
			if (arithmeticExpression != null)
				return new ArithmeticExpressionOutputBlock(arithmeticExpression.Accept(ArithmeticExpressionVisitor.Instance));

			throw new InvalidOperationException("Unknown alternative");
		}

		public override ITemplateNode VisitFilteredParameterValueExpression(QuokkaParser.FilteredParameterValueExpressionContext context)
		{
			var filters = context.filterChain();
			if (filters != null)
				throw new NotImplementedException("Parameters with filter chain are not supported");

			var parameter = context.parameterValueExpression().Accept(new VariableVisitor(VariableType.Primitive));
			return new VariableOutputBlock(parameter);
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

		public override ITemplateNode VisitCommentBlock(QuokkaParser.CommentBlockContext context)
		{
			return null;
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
	}
}
