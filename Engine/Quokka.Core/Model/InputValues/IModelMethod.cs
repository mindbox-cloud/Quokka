using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
    public interface IModelMethod
    {
		string Name { get; }

		IReadOnlyList<object> Arguments { get; }

	    IModelValue Value { get; }
	}
}
