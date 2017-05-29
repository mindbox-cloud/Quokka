using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
    internal class MethodMember : Member
    {
	    private readonly string name;
	    private readonly IReadOnlyList<Argument> arguments;
		private readonly Location location;

	    public MethodMember(string name, IEnumerable<Argument> arguments, Location location)
	    {
		    this.name = name;
		    this.arguments = arguments.ToList().AsReadOnly();
			this.location = location;
	    }

	    public override void CompileMemberVariableDefinition(ValueUsageSummary ownerValueUsageSummary, TypeDefinition memberType)
	    {
		    ownerValueUsageSummary.Methods
			    .CreateOrUpdateMember(TryBuildMethodCall(), new ValueUsage(location, memberType));
	    }

	    public override ValueUsageSummary GetMemberVariableDefinition(ValueUsageSummary ownerValueUsageSummary)
	    {
		    return ownerValueUsageSummary.Methods.TryGetMemberUsageSummary(TryBuildMethodCall());
	    }

	    public override VariableValueStorage GetMemberValue(VariableValueStorage ownerValueStorage)
	    {
		    return ownerValueStorage.GetMethodCallResultValueStorage(TryBuildMethodCall());
	    }

	    public override string StringRepresentation => $"{name}()";

	    private MethodCall TryBuildMethodCall()
	    {
		    var argumentValues = arguments
			    .Select(argument => argument.TryGetStaticValue()?.GetPrimitiveValue())
			    .ToList();

			if (argumentValues.Any(value => value == null))
				throw new InvalidOperationException("Incorrect value. Must be static.");

			return new MethodCall(name, argumentValues);
	    }
    }
}
