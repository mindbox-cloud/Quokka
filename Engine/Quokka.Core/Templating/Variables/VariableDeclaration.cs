namespace Quokka
{
	internal class VariableDeclaration : VariableOccurence
	{
		public override bool IsExternal => false;

		public VariableDeclaration(string name, Location location, VariableType requiredType, VariableOccurence member)
			: base(name, location, requiredType, member)
		{
		}
	}
}
