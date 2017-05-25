using System;
using System.Text;

namespace Mindbox.Quokka.Html
{
	/// <summary>
	/// Block that will be injected in the html code for tracking purposes (most commonly a tracking image).
	/// </summary>
	internal class IdentificationCodePlaceHolderBlock : TemplateNodeBase, IStaticBlockPart
	{
		public int Offset { get; }
		public int Length { get; } = 0;

		public IdentificationCodePlaceHolderBlock(int offset)
		{
			Offset = offset;
		}

		public override void Render(StringBuilder resultBuilder, RenderContext renderContext)
		{
			var htmlRenderContext = (HtmlRenderContext)renderContext;
			if (htmlRenderContext.IdentificationCode != null && !htmlRenderContext.HasIdentificationCodeBeenRendered)
			{
				resultBuilder.Append(htmlRenderContext.IdentificationCode);
				htmlRenderContext.LogIdentificationCodeRendering();
			}
		}
	}
}
