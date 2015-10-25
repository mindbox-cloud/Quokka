using System.Collections.Generic;

namespace Quokka
{
	public interface ICompositeModelValue : IModelValue
	{
		IReadOnlyList<IModelField> Fields { get; }
	}
}