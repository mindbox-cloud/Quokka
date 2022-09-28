// // Copyright 2022 Mindbox Ltd
// //
// // Licensed under the Apache License, Version 2.0 (the "License");
// // you may not use this file except in compliance with the License.
// // You may obtain a copy of the License at
// //
// //     http://www.apache.org/licenses/LICENSE-2.0
// //
// // Unless required by applicable law or agreed to in writing, software
// // distributed under the License is distributed on an "AS IS" BASIS,
// // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// // See the License for the specific language governing permissions and
// // limitations under the License.

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
