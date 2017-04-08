using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
	internal interface IStaticBlockPart : ITemplateNode
	{
		/// <summary>
		/// Offset (zero-based, in characters) from the beginning of the static block that this parts resides in.
		/// </summary>
		int Offset { get; }

		int Length { get; }
	}
}
