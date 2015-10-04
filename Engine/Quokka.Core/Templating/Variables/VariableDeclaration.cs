namespace Quokka
{
	internal abstract class VariableDeclaration : VariableOccurence
	{
		public override bool IsGlobal => false;

		protected VariableDeclaration(string name, VariableType requiredType, VariableOccurence member)
			: base(name, requiredType, member)
		{
		}
	}
}
