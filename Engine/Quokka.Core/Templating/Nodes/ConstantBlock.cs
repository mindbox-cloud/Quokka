using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quokka
{
	internal class ConstantBlock : TemplateNodeBase
	{
		private readonly string text;

		public ConstantBlock(string text)
		{
			this.text = text;
		}
	}
}
