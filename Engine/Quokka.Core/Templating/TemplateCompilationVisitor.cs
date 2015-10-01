using System;
using System.Collections.Generic;

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
			AddDebugMessage("TemplateBlock");
			var childBlocks = new List<ITemplateNode>();

			for (int i = 0; i < context.ChildCount; i++)
				childBlocks.Add(context.GetChild(i).Accept(this));

			return new TemplateBlock(childBlocks);
		}

		public override ITemplateNode VisitStaticBlock(QuokkaParser.StaticBlockContext context)
		{
			AddDebugMessage("StaticBlock");
			return new SomeNode("static");
		}

		public override ITemplateNode VisitDynamicBlock(QuokkaParser.DynamicBlockContext context)
		{
			AddDebugMessage("DynamicBlock");
			return base.VisitDynamicBlock(context);
		}

		public override ITemplateNode VisitConstantBlock(QuokkaParser.ConstantBlockContext context)
		{
			AddDebugMessage(context.GetText());
			return new SomeNode("Constant");
		}

		public override ITemplateNode VisitOutputInstruction(QuokkaParser.OutputInstructionContext context)
		{
			AddDebugMessage("Output instruction");
			return base.VisitOutputInstruction(context);
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
