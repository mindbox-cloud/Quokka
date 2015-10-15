using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Quokka
{
	internal class CompositeModelDefinition : ICompositeModelDefinition
	{
		public static CompositeModelDefinition Empty { get; } =
			new CompositeModelDefinition(
				new ReadOnlyDictionary<string, IModelDefinition>(
					new Dictionary<string, IModelDefinition>()));

		public IReadOnlyDictionary<string, IModelDefinition> Fields { get; }

		public CompositeModelDefinition(IReadOnlyDictionary<string, IModelDefinition> fields)
		{
			Fields = fields;
		}
	}
}
