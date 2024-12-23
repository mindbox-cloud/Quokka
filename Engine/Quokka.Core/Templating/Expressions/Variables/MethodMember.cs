﻿// // Copyright 2022 Mindbox Ltd
// //
// // Licensed under the Apache License, Version 2.0 (the "License");
// // you may not use this file except in compliance with the License.
// // You may obtain a copy of the License at
// //
// //     http://www.apache.org/licenses/LICENSE-2.0
// //
// // Unless required by applicable law or agreed to in writing, software
// // distributed under the License is distributed on an "AS IS" BASIS,
// // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// // See the License for the specific language governing permissions and
// // limitations under the License.

using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
    internal class MethodMember : Member
    {
	    private readonly string name;
	    private readonly IReadOnlyList<ArgumentValue> arguments;

	    private readonly MethodCall methodCall;

	    public MethodMember(string name, IEnumerable<ArgumentValue> arguments, Location location)
			: base(location)
	    {
		    this.name = name;
		    this.arguments = arguments.ToList().AsReadOnly();

		    methodCall = BuildMethodCall();
	    }

	    public override void PerformSemanticAnalysis(AnalysisContext analysisContext, ValueUsageSummary ownerValueUsageSummary, TypeDefinition memberType)
	    {
		    for (int i = 0; i < arguments.Count; i++)
			    if (arguments[i].TryGetStaticValue() == null)
				    analysisContext.ErrorListener.AddNonConstantMethodArgumentError(name, i + 1, Location);

		    ownerValueUsageSummary.Methods
			    .CreateOrUpdateMember(methodCall, new ValueUsage(Location, memberType));
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

		public override void Accept(ITemplateVisitor treeVisitor)
		{
			treeVisitor.VisitMethodMember(name);

			foreach (var argument in arguments)
				argument.Accept(treeVisitor);
			
			treeVisitor.EndVisit();
		}

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
