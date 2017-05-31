using System.Collections.Generic;

namespace Mindbox.Quokka
{
	internal class ModelDefinitionFactory
	{
		public IPrimitiveModelDefinition CreatePrimitive(TypeDefinition type)
		{
			return new PrimitiveModelDefinition(type);
		}

		public ICompositeModelDefinition CreateComposite(
			IReadOnlyDictionary<string, IModelDefinition> fieldDefinitions,
			IReadOnlyDictionary<IMethodCallDefinition, IModelDefinition> methodDefinitions)
		{
			return new CompositeModelDefinition(fieldDefinitions, methodDefinitions);
		}

		public IArrayModelDefinition CreateArray(
			IModelDefinition elementModelDefinition,
			IReadOnlyDictionary<string, IModelDefinition> fieldDefinitions = null,
			IReadOnlyDictionary<IMethodCallDefinition, IModelDefinition> methodDefinitions = null)
		{
			return new ArrayModelDefinition(elementModelDefinition, fieldDefinitions, methodDefinitions);
		}
	}
}