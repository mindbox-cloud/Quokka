using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Quokka
{
	internal class CompositeModelDefinition : ICompositeModelDefinition
	{
		public static CompositeModelDefinition Empty { get; } = new CompositeModelDefinition(
		new ReadOnlyDictionary<string, IModelDefinition>(
			new Dictionary<string, IModelDefinition>()));

		public IDictionary<string, IModelDefinition> Fields { get; }

		public CompositeModelDefinition(IDictionary<string, IModelDefinition> fields)
		{
			Fields = fields;
		}
	}
}
