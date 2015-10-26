using System.Linq;
using Quokka.Generated;

namespace Quokka
{
	internal class StaticBlockVisitor : QuokkaBaseVisitor<StaticBlock>
	{
		public StaticBlockVisitor(VisitingContext visitingContext)
			: base(visitingContext)
		{
		}

		public override StaticBlock VisitStaticBlock(QuokkaParser.StaticBlockContext context)
		{
			return new StaticBlock(context.children.Select(child => child.Accept(new TemplateVisitor(visitingContext))));
		}
	}
}
