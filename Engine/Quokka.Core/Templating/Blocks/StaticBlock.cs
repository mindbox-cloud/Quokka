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
using System.IO;
using System.Linq;
using System.Text;

namespace Mindbox.Quokka
{
	internal class StaticBlock : TemplateNodeBase
	{
		private readonly IReadOnlyCollection<IStaticBlockPart> children;

		public override bool IsConstant
		{
			get { return children.All(child => child.IsConstant); }
		}

		public StaticBlock(IEnumerable<IStaticBlockPart> children)
		{
			this.children = children.ToList().AsReadOnly();
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			foreach (var child in children)
				child.PerformSemanticAnalysis(context);
		}

		public override void Render(TextWriter resultWriter, RenderContext renderContext)
		{
			foreach (var child in children)
				child.Render(resultWriter, renderContext);
		}

		public override void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context)
		{
			foreach (var child in children)
				child.CompileGrammarSpecificData(context);
		}
	}
}
