using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quokka
{
	internal class SomeNode : TemplateNodeBase
	{
		private readonly string nodeKind;

		public SomeNode(string nodeKind)
		{
			this.nodeKind = nodeKind;
		}
	}
}
