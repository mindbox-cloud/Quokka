using System.Collections.Generic;

namespace Quokka
{
	public interface ICompositeModelDefinition : IModelDefinition
	{
		IReadOnlyDictionary<string, IModelDefinition> Fields { get; }
	}
}
