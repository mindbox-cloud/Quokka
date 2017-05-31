using System.Collections.Generic;

namespace Mindbox.Quokka
{
	public interface IArrayModelValue : ICompositeModelValue
	{
		IList<IModelValue> Elements { get; }
	}
}