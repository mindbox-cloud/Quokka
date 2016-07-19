using System.Collections.Generic;

namespace Quokka.Html
{
	internal class AttributeValue
	{
		/// <summary>
		/// Attribute value may consist of multiple constant blocks and parameter/expression output blocks,
		/// e.g. "http://${ domain }/index.${ extension }".
		/// </summary>
		public IReadOnlyList<ITemplateNode> TextComponents { get; }

		public string Text { get;}

		public int Offset { get; }
		public int Length { get; }

		public AttributeValue(IReadOnlyList<ITemplateNode> textComponents, string rawText, int offset, int length)
		{
			TextComponents = textComponents;
			Text = rawText.Trim();
			Offset = offset;
			Length = length;
		}
	}
}
