using System.Collections.Generic;

namespace Quokka
{
	public interface ICompositeModelDefinition : IModelDefinition
	{
		IDictionary<string, IModelDefinition> Fields { get; }
	}
}
