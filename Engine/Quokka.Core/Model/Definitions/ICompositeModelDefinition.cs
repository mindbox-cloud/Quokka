using System.Collections.Generic;

namespace Mindbox.Quokka
{
	public interface ICompositeModelDefinition : IModelDefinition
	{
		IReadOnlyDictionary<string, IModelDefinition> Fields { get; }
	}
}
