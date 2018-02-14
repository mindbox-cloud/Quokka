using System;

namespace Mindbox.Quokka
{
	internal enum VariableUsageIntention
	{
		Read = 0,
		Write = 1
	}

	internal class ValueUsage
	{
		public Location Location { get; }
		public TypeDefinition RequiredType { get; }
		public VariableUsageIntention Intention { get; set; }

		public ValueUsage(
			Location location, 
			TypeDefinition requiredType, 
			VariableUsageIntention intention = VariableUsageIntention.Read)
		{ 
			Location = location;
			RequiredType = requiredType;
			Intention = intention;
		}
	}
}
