using System.Linq;

using Quokka.Generated;

namespace Quokka
{
	internal class RootTemplateVisitor : QuokkaBaseVisitor<TemplateBlock>
	{
		public override TemplateBlock VisitTemplateBlock(QuokkaParser.TemplateBlockContext context)
		{
			var templateCompilationVisitor = TemplateCompilationVisitor.Instance;
			return new TemplateBlock(context.children
				.Select(child => child.Accept(templateCompilationVisitor))
				.Where(x => x != null));
		}
	}
}
