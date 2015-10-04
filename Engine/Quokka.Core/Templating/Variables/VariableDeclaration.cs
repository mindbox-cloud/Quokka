using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
