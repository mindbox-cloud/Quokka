using System;

namespace Quokka
{
	internal class VariableDeclaration : VariableOccurence
	{
		public override bool IsExternal => false;

		public VariableDeclaration(string name, Location location, TypeDefinition requiredType, VariableOccurence member)
			: base(name, location, requiredType, member)
		{
		}
		
		public override VariableOccurence CloneWithSpecificLeafType(TypeDefinition leafMemberType)
		{
			// Shouldn't be called for variable declarations.
			throw new NotImplementedException();
		}
	}
}
