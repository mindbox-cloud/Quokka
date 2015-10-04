namespace Quokka
{
	internal abstract class VariableDeclaration : VariableOccurence
	{
		protected VariableDeclaration(string name, VariableType requiredType, VariableOccurence member)
			: base(name, requiredType, member)
		{
		}
	}
}
