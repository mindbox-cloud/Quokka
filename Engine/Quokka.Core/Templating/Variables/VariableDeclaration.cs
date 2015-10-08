namespace Quokka
{
	internal class VariableDeclaration : VariableOccurence
	{
		public override bool IsGlobal => false;

		public VariableDeclaration(string name, VariableType requiredType, VariableOccurence member)
			: base(name, requiredType, member)
		{
		}
	}
}
