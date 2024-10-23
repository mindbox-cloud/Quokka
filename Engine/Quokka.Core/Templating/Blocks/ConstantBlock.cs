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

using System.IO;

namespace Mindbox.Quokka
{
	internal class ConstantBlock : TemplateNodeBase, IStaticBlockPart
	{
		public string Text { get; }
		public int Offset { get; }
		public int Length { get; }

		public override bool IsConstant { get; } = true;

		public ConstantBlock(string text, int offset, int length)
		{
			Text = text;
			Offset = offset;
			Length = length;
		}

		public override void Render(TextWriter resultWriter, RenderContext renderContext)
		{
			resultWriter.Write(Text);
		}

		public override void Accept(ITemplateVisitor treeVisitor)
		{
			treeVisitor.VisitConstantBlock(Text);
			treeVisitor.EndVisit();
		}
	}
}
