using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
    internal class LengthTemplateFunction : ScalarTemplateFunction<string, int>
    {
		public LengthTemplateFunction() 
			: base("length", new StringFunctionArgument("string", false))
		{
		}

		public override int Invoke(string value)
		{
			return value.Length;
		}
	}
}
