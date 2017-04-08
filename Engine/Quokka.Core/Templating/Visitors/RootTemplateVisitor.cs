using System.Linq;

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
			var templateCompilationVisitor = new TemplateVisitor(visitingContext);
			return new TemplateBlock(context.children
				.Select(child => child.Accept(templateCompilationVisitor))
				.Where(x => x != null));
		}
	}
}
