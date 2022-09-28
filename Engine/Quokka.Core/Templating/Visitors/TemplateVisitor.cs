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

using System.Collections.Generic;
using System.Linq;
using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class TemplateVisitor : QuokkaBaseVisitor<ITemplateNode>
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

		public override ITemplateNode VisitDynamicBlock(QuokkaParser.DynamicBlockContext context)
		{
			return context.Accept(new DynamicBlockVisitor(VisitingContext));
		}
	}
}
