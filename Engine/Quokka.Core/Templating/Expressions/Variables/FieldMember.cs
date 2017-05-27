using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
    internal class FieldMember : Member
    {
	    private readonly string fieldName;

	    private readonly Location location;

	    public FieldMember(string fieldName, Location location)
	    {
		    this.fieldName = fieldName;
		    this.location = location;
	    }

	    public override string StringRepresentation => fieldName;

		public override void CompileMemberVariableDefinition(ValueUsageSummary ownerValueUsageSummary, TypeDefinition memberType)
	    {
		    ownerValueUsageSummary.Fields
			    .CreateOrUpdateMember(fieldName, new ValueUsage(location, memberType));
	    }

	    public override ValueUsageSummary GetMemberVariableDefinition(ValueUsageSummary ownerValueUsageSummary)
	    {
		    return ownerValueUsageSummary.Fields.TryGetMemberUsageSummary(fieldName);
	    }

	    public override VariableValueStorage GetMemberValue(VariableValueStorage ownerValueStorage)
	    {
		    return ownerValueStorage.GetFieldValueStorage(fieldName);
	    }
    }
}
