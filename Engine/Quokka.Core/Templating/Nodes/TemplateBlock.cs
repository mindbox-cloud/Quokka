using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quokka
{
	internal class TemplateBlock : TemplateNodeBase
	{
		private readonly IReadOnlyCollection<ITemplateNode> blocks;

		public TemplateBlock(IEnumerable<ITemplateNode> childBlocks)
		{
			blocks = childBlocks
				.ToList()
				.AsReadOnly();
		}
	}
}
