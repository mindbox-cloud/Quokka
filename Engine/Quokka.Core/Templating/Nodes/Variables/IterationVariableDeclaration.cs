using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			if (collectionVariable == null)
				throw new ArgumentNullException(nameof(collectionVariable));
			if (collectionVariable.RequiredType != VariableType.Array)
				throw new InvalidOperationException("collectionVariable.RequiredType != VariableType.Array");

			this.collectionVariable = collectionVariable;
		}
	}
}
