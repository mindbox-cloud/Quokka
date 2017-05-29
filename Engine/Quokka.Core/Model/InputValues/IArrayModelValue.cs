using System.Collections.Generic;

namespace Mindbox.Quokka
{
	public interface IArrayModelValue : IModelValue
	{
		IList<IModelValue> Elements { get; }
	}
}