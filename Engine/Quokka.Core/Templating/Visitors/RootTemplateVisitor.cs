using System.Linq;

using Antlr4.Runtime.Tree;

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class RootTemplateVisitor : QuokkaBaseVisitor<TemplateBlock>
	{
		public RootTemplateVisitor(VisitingContext visitingContext)
			: base(visitingContext)
		{
		}

		public override TemplateBlock VisitTemplateBlock(QuokkaParser.TemplateBlockContext context)
		{
			var templateVisitor = new TemplateVisitor(VisitingContext);
			return new TemplateBlock(context.children
				.Select(child => child.Accept(templateVisitor))
				.Where(x => x != null));
		}

		public override TemplateBlock VisitTerminal(ITerminalNode node)
		{
			return TemplateBlock.Empty();
		}

		protected override bool ShouldVisitNextChild(IRuleNode node, TemplateBlock currentResult)
		{
			// Ensures that only first present node is visited, either a TemplateBlock node or a terminal EOF node.
			return currentResult == null;
		}
	}
}
