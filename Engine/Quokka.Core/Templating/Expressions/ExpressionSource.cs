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

namespace Mindbox.Quokka
{
	/// <summary>
	/// The place an expression comes from within the template text. Kept by the compiled expression
	/// so that runtime errors can point at the exact expression which failed.
	/// </summary>
	internal sealed class ExpressionSource
	{
		public Location Location { get; }

		/// <summary>
		/// The expression as it is written in the template. <c>Null</c> for expressions
		/// whose text can't be restored from the parse tree.
		/// </summary>
		public string Text { get; }

		public ExpressionSource(Location location, string text)
		{
			Location = location;
			Text = text;
		}
	}
}
