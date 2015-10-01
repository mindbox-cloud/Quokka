using System;
using System.Collections.Generic;
using System.Linq;

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

using Quokka.Generated;

namespace Quokka
{
	internal class TemplateCompilationVisitor : QuokkaBaseVisitor<ITemplateNode>
	{
		private readonly List<string> debugMessages = new List<string>();

		public IEnumerable<string> DebugMessages => debugMessages.AsReadOnly();

		public override ITemplateNode VisitTemplate(QuokkaParser.TemplateContext context)
		{
			AddDebugMessage("Template");
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
			AddDebugMessage("Output block");

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
			AddDebugMessage("If block");
			return new SomeNode("if");
		}

		private void AddDebugMessage(string message)
		{
			if (message == null)
				throw new ArgumentNullException(nameof(message));

			debugMessages.Add(message);
		}
	}
}
