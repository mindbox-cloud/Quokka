using System.Collections.Generic;

namespace Mindbox.Quokka
{
	public interface IArrayModelValue : IModelValue
	{
		IList<IModelValue> Values { get; }
	}
}