using System;

namespace Mindbox.Quokka
{
	internal class VariableDeclaration : VariableOccurence
	{
		public override bool IsExternal => false;

		public VariableDeclaration(string name, Location location, TypeDefinition requiredType)
			: base(name, location, requiredType)
		{
		}
	}
}
