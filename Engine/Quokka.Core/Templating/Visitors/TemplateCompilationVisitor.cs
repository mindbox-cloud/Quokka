﻿using System;
using System.Collections.Generic;
using System.Linq;
using Quokka.Generated;

namespace Quokka
{
	internal class TemplateCompilationVisitor : QuokkaBaseVisitor<ITemplateNode>
	{
		public override ITemplateNode VisitTemplate(QuokkaParser.TemplateContext context)
		{
			return new TemplateRoot((TemplateBlock)context.templateBlock().Accept(this));
		}

		public override ITemplateNode VisitTemplateBlock(QuokkaParser.TemplateBlockContext context)
		{
			return new TemplateBlock(
				context.children.Select(child => child.Accept(this)));
		}

		public override ITemplateNode VisitStaticBlock(QuokkaParser.StaticBlockContext context)
		{
			return new StaticBlock(
				context.children.Select(child => child.Accept(this)),
				context.GetText());
		}
		
		public override ITemplateNode VisitConstantBlock(QuokkaParser.ConstantBlockContext context)
		{
			return new ConstantBlock(context.GetText());
		}

		public override ITemplateNode VisitOutputBlock(QuokkaParser.OutputBlockContext context)
		{
			return context.filteredParameterValueExpression()?.Accept(this) ??
					context.arithmeticExpression()?.Accept(this);
		}

		public override ITemplateNode VisitFilteredParameterValueExpression(QuokkaParser.FilteredParameterValueExpressionContext context)
		{
			var filters = context.filterChain();
			if (filters != null)
				throw new NotImplementedException("Parameters with filter chain are not supported");

			var parameter = context.parameterValueExpression().Accept(new ParameterVisitor(ParameterType.Primitive));
			return new ParameterOutputBlock(parameter);
		}

		public override ITemplateNode VisitIfStatement(QuokkaParser.IfStatementContext context)
		{
			var conditionsVisitor = new ConditionsVisitor();

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
	}
}
