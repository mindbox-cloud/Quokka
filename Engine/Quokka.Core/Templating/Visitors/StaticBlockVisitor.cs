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
