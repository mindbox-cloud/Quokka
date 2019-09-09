using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mindbox.Quokka
{
	internal class CompositeModelDefinition : ICompositeModelDefinition
	{
		public static CompositeModelDefinition Empty { get; } =
			new CompositeModelDefinition(
				new ReadOnlyDictionary<string, IModelDefinition>(
					new Dictionary<string, IModelDefinition>()),
				new ReadOnlyDictionary<IMethodCallDefinition, IModelDefinition>(
					new Dictionary<IMethodCallDefinition, IModelDefinition>()));

		public IReadOnlyDictionary<string, IModelDefinition> Fields { get; }
		public IReadOnlyDictionary<IMethodCallDefinition, IModelDefinition> Methods { get; }

		public CompositeModelDefinition(
			IReadOnlyDictionary<string, IModelDefinition>? fields = null,
			IReadOnlyDictionary<IMethodCallDefinition, IModelDefinition>? methods = null)
		{
			Fields = fields ?? new Dictionary<string, IModelDefinition>();
			Methods = methods ?? new Dictionary<IMethodCallDefinition, IModelDefinition>();
		}
	}
}
