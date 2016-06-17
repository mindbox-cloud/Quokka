using System;

namespace Quokka
{
	public class CollectionArgument : TemplateFunctionArgument
	{
		public CollectionArgument(string name)
			: base(name)
		{
		}

		internal override TypeDefinition Type => TypeDefinition.Array;

		internal override ArgumentValueValidationResult ValidateValue(VariableValueStorage value)
		{
			// Shouldn't be called because this argument couldn't possibly receive a static scalar value known at compile-time
			throw new NotImplementedException();
		}
	}
}
