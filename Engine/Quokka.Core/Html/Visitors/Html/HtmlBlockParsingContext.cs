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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka.Html
{
	internal class HtmlBlockParsingContext
	{
		/// <summary>
		/// Output blocks like ${....}  are parsed by external (base) grammar before we get into subgrammar (HTML).
		/// From HTML point of view ${...} block is just a token. This map allows us match such tokens with pre-parsed
		/// parts of template tree based on their offset inside the HTML block.
		/// </summary>
		public IReadOnlyDictionary<int, OutputInstructionBlock> PreparsedOutputBlockNodes { get; }

		public HtmlBlockParsingContext(IReadOnlyDictionary<int, OutputInstructionBlock> preparsedOutputBlockNodes)
		{
			PreparsedOutputBlockNodes = preparsedOutputBlockNodes;
		}
	}
}
