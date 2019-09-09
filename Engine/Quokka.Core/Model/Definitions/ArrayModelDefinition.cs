using System.Collections.Generic;

namespace Mindbox.Quokka
{
	internal class ArrayModelDefinition : CompositeModelDefinition, IArrayModelDefinition
	{
		public IModelDefinition ElementModelDefinition { get; }

		public ArrayModelDefinition(
			IModelDefinition elementModelDefinition,
			IReadOnlyDictionary<string, IModelDefinition>? fields = null,
			IReadOnlyDictionary<IMethodCallDefinition, IModelDefinition>? methods = null)
			:base(fields, methods)
		{
			ElementModelDefinition = elementModelDefinition;
		}
	}
}
