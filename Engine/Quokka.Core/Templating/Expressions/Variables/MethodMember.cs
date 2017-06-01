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
	    private readonly IReadOnlyList<ArgumentValue> arguments;
		private readonly Location location;

	    private readonly MethodCall methodCall;

	    public MethodMember(string name, IEnumerable<ArgumentValue> arguments, Location location)
	    {
		    this.name = name;
		    this.arguments = arguments.ToList().AsReadOnly();
			this.location = location;

		    methodCall = BuildMethodCall();
	    }

	    public override void PerformSemanticAnalysis(AnalysisContext analysisContext, ValueUsageSummary ownerValueUsageSummary, TypeDefinition memberType)
	    {
		    for (int i = 0; i < arguments.Count; i++)
			    if (arguments[i].TryGetStaticValue() == null)
				    analysisContext.ErrorListener.AddNonConstantMethodArgumentError(name, i + 1, location);

		    ownerValueUsageSummary.Methods
			    .CreateOrUpdateMember(methodCall, new ValueUsage(location, memberType));
	    }

	    public override ValueUsageSummary GetMemberVariableDefinition(ValueUsageSummary ownerValueUsageSummary)
	    {
		    return ownerValueUsageSummary.Methods.TryGetMemberUsageSummary(methodCall);
	    }

	    public override VariableValueStorage GetMemberValue(VariableValueStorage ownerValueStorage)
	    {
		    return ownerValueStorage.GetMethodCallResultValueStorage(methodCall);
	    }

	    public override string StringRepresentation => $"{name}()";

	    private MethodCall BuildMethodCall()
	    {
		    var argumentValues = arguments
			    .Select(argument => argument.TryGetStaticValue()?.GetPrimitiveValue())
				.Where(argumentValue => argumentValue != null)
			    .ToList();

		    return new MethodCall(name, argumentValues);
	    }
    }
}
