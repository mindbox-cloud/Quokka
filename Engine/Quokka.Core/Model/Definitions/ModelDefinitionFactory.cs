using System.Collections.Generic;

namespace Quokka
{
	internal class ModelDefinitionFactory : IModelDefinitionFactory
	{
		public IPrimitiveModelDefinition CreatePrimitive(TypeDefinition type)
		{
			return new PrimitiveModelDefinition(type);
		}

		public ICompositeModelDefinition CreateComposite(IReadOnlyDictionary<string, IModelDefinition> fieldDefinitions)
		{
			return new CompositeModelDefinition(fieldDefinitions);
		}

		public IArrayModelDefinition CreateArray(IModelDefinition elementModelDefinition)
		{
			return new ArrayModelDefinition(elementModelDefinition);
		}
	}
}