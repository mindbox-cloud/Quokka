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

		public override void CompileMemberVariableDefinition(VariableDefinition ownerVariableDefinition, TypeDefinition memberType)
	    {
		    ownerVariableDefinition.Fields
			    .CreateOrUpdateVariableDefinition(
				    new VariableOccurence(fieldName, location, memberType));
	    }

	    public override VariableDefinition GetMemberVariableDefinition(VariableDefinition ownerVariableDefinition)
	    {
		    return ownerVariableDefinition.Fields.TryGetVariableDefinition(fieldName);
	    }

	    public override VariableValueStorage GetMemberValue(VariableValueStorage ownerValueStorage)
	    {
		    return ownerValueStorage.GetMemberValueStorage(fieldName);
	    }
    }
}
