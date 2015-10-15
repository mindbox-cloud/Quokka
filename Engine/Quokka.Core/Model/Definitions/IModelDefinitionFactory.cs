using System.Collections.Generic;

namespace Quokka
{
	public interface IModelDefinitionFactory
	{
		IPrimitiveModelDefinition CreatePrimitive(TypeDefinition type);

		ICompositeModelDefinition CreateComposite(IReadOnlyDictionary<string, IModelDefinition> fieldDefinitions);

		IArrayModelDefinition CreateArray(IModelDefinition elementModelDefinition);
	}
}
