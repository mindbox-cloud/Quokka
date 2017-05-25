using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
    internal abstract class Member
    {
	    public abstract void CompileMemberVariableDefinition(
			VariableDefinition ownerVariableDefinition,
			TypeDefinition memberType);

	    public abstract VariableDefinition GetMemberVariableDefinition(VariableDefinition ownerVariableDefinition);

	    public abstract VariableValueStorage GetMemberValue(VariableValueStorage ownerValueStorage);

		public abstract string StringRepresentation { get; }
    }
}
