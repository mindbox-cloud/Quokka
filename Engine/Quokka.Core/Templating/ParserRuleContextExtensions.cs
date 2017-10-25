using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;

namespace Mindbox.Quokka
{
    internal static class ParserRuleContextExtensions 
    {
		public static int GetContextLength(this ParserRuleContext context)
		{
			return context.Stop.StopIndex - context.Start.StartIndex + 1;
		}
    }
}
