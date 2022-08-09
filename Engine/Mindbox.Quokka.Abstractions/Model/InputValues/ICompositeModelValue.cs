using System.Collections.Generic;

namespace Mindbox.Quokka
{
	public interface ICompositeModelValue : IModelValue
	{
		IEnumerable<IModelField> Fields { get; }

		IEnumerable<IModelMethod> Methods { get; }
	}
}