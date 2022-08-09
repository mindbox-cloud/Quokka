using System.Collections.Generic;

namespace Mindbox.Quokka
{
    public interface IModelMethod
    {
		string Name { get; }

		IReadOnlyList<object> Arguments { get; }

	    IModelValue Value { get; }
	}
}
