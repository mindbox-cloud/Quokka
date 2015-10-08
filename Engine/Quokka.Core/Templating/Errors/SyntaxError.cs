using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quokka
{
	internal class SyntaxError : TemplateErrorBase
	{
		public SyntaxError(string message, int line, int column)
			: base(message, line, column)
		{
		}
	}
}
