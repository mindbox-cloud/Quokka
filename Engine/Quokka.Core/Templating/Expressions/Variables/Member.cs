using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
    internal abstract class Member
    {
		protected Member(Location location)
		{
			Location = location;
		}

	    public abstract void PerformSemanticAnalysis(
			AnalysisContext analysisContext,
			ValueUsageSummary ownerValueUsageSummary,
			TypeDefinition memberType);

	    public abstract ValueUsageSummary GetMemberVariableDefinition(ValueUsageSummary ownerValueUsageSummary);

	    public abstract VariableValueStorage GetMemberValue(VariableValueStorage ownerValueStorage);

		public abstract string StringRepresentation { get; }

		public Location Location { get; }
    }
}
