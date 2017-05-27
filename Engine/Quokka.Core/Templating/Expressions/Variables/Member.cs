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
			ValueUsageSummary ownerValueUsageSummary,
			TypeDefinition memberType);

	    public abstract ValueUsageSummary GetMemberVariableDefinition(ValueUsageSummary ownerValueUsageSummary);

	    public abstract VariableValueStorage GetMemberValue(VariableValueStorage ownerValueStorage);

		public abstract string StringRepresentation { get; }
    }
}
