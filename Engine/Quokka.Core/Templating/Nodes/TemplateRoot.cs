using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Quokka
{
	internal class TemplateRoot : TemplateNodeBase
	{
		private readonly TemplateBlock templateBlock;

		public TemplateRoot(TemplateBlock templateBlock)
		{
			this.templateBlock = templateBlock;
		}
	}
}
