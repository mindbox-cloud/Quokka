using System;

namespace Mindbox.Quokka
{
	internal class ValueUsage
	{
		public Location Location { get; }
		public TypeDefinition RequiredType { get; }

		public ValueUsage(Location location, TypeDefinition requiredType)
		{ 
			Location = location;
			RequiredType = requiredType;
		}
	}
}
