using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quokka
{
	internal class ConditionBlock : TemplateNodeBase
	{
		private readonly ITemplateNode content;

		public ConditionBlock(ITemplateNode content)
		{
			this.content = content;
		}
	}
}
