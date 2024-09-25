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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mindbox.Quokka
{
	internal class TemplateBlock : TemplateNodeBase
	{
		public readonly IReadOnlyCollection<ITemplateNode> children;

		public override bool IsConstant
		{
			get { return children.All(child => child.IsConstant); }
		}

		public TemplateBlock(IEnumerable<ITemplateNode> children)
		{
			this.children = children
				.ToList()
				.AsReadOnly();
			// Console.WriteLine(ObjectDumper.Dump(children, DumpStyle.CSharp));

		}

		public static TemplateBlock Empty()
		{
			return new TemplateBlock(Enumerable.Empty<ITemplateNode>());
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			foreach (var child in children)
				child.PerformSemanticAnalysis(context);
		}

		public override void Render(TextWriter resultWriter, RenderContext renderContext)
		{

			// Console.WriteLine(ObjectDumper.Dump(children, DumpStyle.CSharp));
			foreach (var child in children)
				child.Render(resultWriter, renderContext);
		}

		public override void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context)
		{
			foreach (var child in children)
				child.CompileGrammarSpecificData(context);
		}

		public override BlockDTO GetTreeDTO()
		{
			var dto = new BlockDTO()
			{
				type = "TemplateBlock",
				children = children.Select(child => child.GetTreeDTO()).ToList()
			};

			return dto;
		}
	}
}
