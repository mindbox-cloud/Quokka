using System.Collections.Generic;

namespace Quokka
{
	public interface IArrayModelValue : IModelValue
	{
		IList<IModelValue> Values { get; }
	}
}