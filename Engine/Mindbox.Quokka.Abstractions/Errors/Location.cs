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
	/// Location in a template string
	/// </summary>
	public sealed class Location
	{
		/// <summary>
		/// Line index (1-based)
		/// </summary>
		public int Line { get; }

		/// <summary>
		/// Column index (1-based)
		/// </summary>
		public int Column { get; }

		public Location(int line, int column)
		{
			Line = line;
			Column = column;
		}

		public override string ToString()
			=> $"{Line}:{Column}";
	}
}
