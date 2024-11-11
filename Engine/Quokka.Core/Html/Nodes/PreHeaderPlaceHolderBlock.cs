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

namespace Mindbox.Quokka.Html;

internal class PreHeaderPlaceHolderBlock(int offset) : TemplateNodeBase, IStaticBlockPart
{
    public int Offset { get; } = offset;
    public int Length => 0;

    public override void Render(TextWriter resultWriter, RenderContext renderContext)
    {
        var htmlRenderContext = (HtmlRenderContext)renderContext;
        if (htmlRenderContext.PreHeader != null && !htmlRenderContext.HasIdentificationCodeBeenRendered)
        {
            resultWriter.Write(htmlRenderContext.PreHeader);
            htmlRenderContext.LogPreHeaderRendering();
        }
    }

    public override void Accept(ITemplateVisitor treeVisitor)
    {
        treeVisitor.VisitPreHeaderPlaceHolderBlock();
        treeVisitor.EndVisit();
    }
}