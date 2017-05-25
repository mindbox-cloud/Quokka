using System.Linq;
using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class StaticBlockVisitor : QuokkaBaseVisitor<StaticBlock>
	{
		public StaticBlockVisitor(VisitingContext visitingContext)
			: base(visitingContext)
		{
		}

		public override StaticBlock VisitStaticBlock(QuokkaParser.StaticBlockContext context)
		{
			return new StaticBlock(context.children
				.Select(child => child.Accept(new StaticPartVisitor(VisitingContext, context.Start.StartIndex))));
		}
	}
}
