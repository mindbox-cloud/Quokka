using System;

namespace Quokka
{
	internal class IterationVariableDeclaration : VariableDeclaration
	{
		private readonly VariableOccurence collectionVariable;

		public IterationVariableDeclaration(
			string name,
			VariableType requiredType,
			VariableOccurence member,
			VariableOccurence collectionVariable)
			: base(name, requiredType, member)
		{
			var leafMember = collectionVariable.GetLeafMember();
			if (leafMember.RequiredType != VariableType.Array)
				throw new InvalidOperationException("leafMember.RequiredType != VariableType.Array");

			this.collectionVariable = collectionVariable;
		}
	}
}
