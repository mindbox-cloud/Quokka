using System;

namespace Quokka
{
	internal class VariableOccurence
	{
		public string Name { get; }
		public Location Location { get; }
		public VariableType RequiredType { get; }
		public VariableOccurence Member { get; }

		/// <summary>
		/// Shows if the variable represents a part of external data required to be passed into template.
		/// First-level external variables go into global scope while internal variables are declared in the inner-most scope.
		/// </summary>
		public virtual bool IsExternal => true;

		public VariableOccurence(string name, Location location, VariableType requiredType, VariableOccurence member)
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

		public virtual VariableOccurence CloneWithSpecificLeafType(VariableType leafMemberType)
		{
			if (Member == null)
			{
				if (RequiredType != VariableType.Primitive)
					throw new InvalidOperationException("Trying to redefine a type of a variable with known concrete type");

				return new VariableOccurence(Name, Location, leafMemberType, null);
			}

			return new VariableOccurence(Name, Location, RequiredType, Member.CloneWithSpecificLeafType(leafMemberType));
		}
	}
}
