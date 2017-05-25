using System;

namespace Mindbox.Quokka
{
	internal class VariableOccurence
	{
		public string Name { get; }
		public Location Location { get; }
		public TypeDefinition RequiredType { get; }

		/// <summary>
		/// Shows if the variable represents a part of external data required to be passed into template.
		/// First-level external variables go into global scope while internal variables are declared in the inner-most scope.
		/// </summary>
		public virtual bool IsExternal => true;

		public VariableOccurence(string name, Location location, TypeDefinition requiredType)
		{ 
			Name = name;
			Location = location;
			RequiredType = requiredType;
		}
		public override string ToString()
		{
			return Name;
		}
	}
}
