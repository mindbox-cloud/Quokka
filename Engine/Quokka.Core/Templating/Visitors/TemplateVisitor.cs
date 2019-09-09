using System.Collections.Generic;
using System.Linq;
using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class TemplateVisitor : QuokkaBaseVisitor<ITemplateNode?>
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
			return context.Accept(VisitingContext.CreateStaticBlockVisitor());
		}

		public override ITemplateNode? VisitDynamicBlock(QuokkaParser.DynamicBlockContext context)
		{
			return context.Accept(new DynamicBlockVisitor(VisitingContext));
		}
	}
}
