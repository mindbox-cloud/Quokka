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
