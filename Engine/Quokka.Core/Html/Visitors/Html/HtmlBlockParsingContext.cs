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
