using System.Collections.Generic;

namespace Mindbox.Quokka
{
	public interface ICompositeModelValue : IModelValue
	{
		IReadOnlyList<IModelField> Fields { get; }
	}
}