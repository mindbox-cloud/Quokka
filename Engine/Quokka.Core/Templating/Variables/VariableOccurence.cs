using System;

namespace Quokka
{
	internal class VariableOccurence
	{
		public string Name { get; }
		public Location Location { get; }
		public TypeDefinition RequiredType { get; }
		public VariableOccurence Member { get; }

		/// <summary>
		/// Shows if the variable represents a part of external data required to be passed into template.
		/// First-level external variables go into global scope while internal variables are declared in the inner-most scope.
		/// </summary>
		public virtual bool IsExternal => true;

		public VariableOccurence(string name, Location location, TypeDefinition requiredType, VariableOccurence member)
		{ 
			Name = name;
			Location = location;
			RequiredType = requiredType;

			if (member != null)
				Member = member;
		}

		public VariableOccurence GetLeafMember()
		{
			return Member == null ? this : Member.GetLeafMember();
		}

		public string GetLeafMemberFullName()
		{
			return Member == null 
				? Name 
				: $"{Name}.{Member.GetLeafMemberFullName()}";
		}

		public string GetMemberFullName(VariableOccurence member)
		{
			return member == this 
				? Name 
				: $"{Name}.{Member.GetMemberFullName(member)}";
		}

		public virtual VariableOccurence CloneWithSpecificLeafType(TypeDefinition leafMemberType)
		{
			if (Member == null)
			{
				if (RequiredType != TypeDefinition.Primitive)
					throw new InvalidOperationException("Trying to redefine a type of a variable with known concrete type");

				return new VariableOccurence(Name, Location, leafMemberType, null);
			}

			return new VariableOccurence(Name, Location, RequiredType, Member.CloneWithSpecificLeafType(leafMemberType));
		}

		public override string ToString()
		{
			return Member == null ? Name : $"{Name}.{Member}";
		}
	}
}
