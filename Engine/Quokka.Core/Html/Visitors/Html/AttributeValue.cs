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

namespace Mindbox.Quokka.Html
{
	internal class AttributeValue
	{
		/// <summary>
		/// Attribute value may consist of multiple constant blocks and parameter/expression output blocks,
		/// e.g. "http://${ domain }/index.${ extension }".
		/// </summary>
		public IReadOnlyList<ITemplateNode> TextComponents { get; }

		public string Text { get;}
		public bool IsQuoted { get; }
		public int Offset { get; }
		public int Length { get; }
		public Location Location { get; }

		public AttributeValue(
			IReadOnlyList<ITemplateNode> textComponents, 
			string rawText, 
			int offset, 
			int length,
			bool isQuoted,
			Location location)
		{
			TextComponents = textComponents;
			Text = rawText.Trim();
			Offset = offset;
			Length = length;
			IsQuoted = isQuoted;
			Location = location;
		}
	}
}
