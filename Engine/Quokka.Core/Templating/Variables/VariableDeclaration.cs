using System;

namespace Mindbox.Quokka
{
	internal class VariableDeclaration : ValueUsage
	{
		public override bool IsExternal => false;
		public string Name { get; }

		public VariableDeclaration(string name, Location location, TypeDefinition requiredType)
			: base(location, requiredType)
		{
			Name = name;
		}
	}
}
