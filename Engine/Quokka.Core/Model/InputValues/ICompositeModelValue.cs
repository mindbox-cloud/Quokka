using System.Collections.Generic;

namespace Quokka
{
	public interface ICompositeModelValue : IModelValue
	{
		IList<IModelField> Fields { get; }
	}
}