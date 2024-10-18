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
using System.IO;
using System.Text;

namespace Mindbox.Quokka.Html
{
	/// <summary>
	/// Block that will be injected in the html code for tracking purposes (most commonly a tracking image).
	/// </summary>
	internal class IdentificationCodePlaceHolderBlock : TemplateNodeBase, IStaticBlockPart
	{
		public int Offset { get; }
		public int Length => 0;

		public IdentificationCodePlaceHolderBlock(int offset)
		{
			Offset = offset;
		}

		public override void Render(TextWriter resultWriter, RenderContext renderContext)
		{
			var htmlRenderContext = (HtmlRenderContext)renderContext;
			if (htmlRenderContext.IdentificationCode != null && !htmlRenderContext.HasIdentificationCodeBeenRendered)
			{
				resultWriter.Write(htmlRenderContext.IdentificationCode);
				htmlRenderContext.LogIdentificationCodeRendering();
			}
		}

		public override void Accept(ITreeVisitor treeVisitor)
		{
			treeVisitor.VisitIdentificationCodePlaceHolderBlock();
			treeVisitor.EndVisit();
		}
	}
}
