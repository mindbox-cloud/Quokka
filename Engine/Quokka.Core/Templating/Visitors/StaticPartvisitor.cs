using System;

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class StaticPartVisitor : QuokkaBaseVisitor<IStaticBlockPart>
	{
		private readonly int staticBlockOffset;

		public StaticPartVisitor(VisitingContext visitingContext, int staticBlockOffset)
			: base(visitingContext)
		{
			this.staticBlockOffset = staticBlockOffset;
		}

		public override IStaticBlockPart VisitConstantBlock(QuokkaParser.ConstantBlockContext context)
		{
			var text = context.GetText();
			return new ConstantBlock(
				text, 
				GetRelativePartOffset(context.Start.StartIndex),
				context.GetContextLength());
		}

		public override IStaticBlockPart VisitOutputBlock(QuokkaParser.OutputBlockContext context)
		{
			var outputExpression =
				context.filterChain() != null
					? context.Accept(new FilterChainVisitor(VisitingContext))
					: context.expression().Accept(new ExpressionVisitor(VisitingContext));

			var startIndex = context.Start.StartIndex;
			var length = context.GetContextLength();

			return new OutputInstructionBlock(outputExpression, GetRelativePartOffset(startIndex), length);
		}

		private int GetRelativePartOffset(int absoluteOffset)
		{
			return absoluteOffset - staticBlockOffset;
		}
	}
}
