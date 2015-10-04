namespace Quokka
{
	internal class VariableOccurence
	{
		public string Name { get; }
		public VariableType RequiredType { get; }
		public VariableOccurence Member { get; }

		public VariableOccurence(string name, VariableType requiredType, VariableOccurence member)
		{ 
			Name = name;
			RequiredType = requiredType;

			if (member != null)
				Member = member;
		}

		public VariableOccurence GetLeafMember()
		{
			return Member == null ? this : Member.GetLeafMember();
		}
	}
}
