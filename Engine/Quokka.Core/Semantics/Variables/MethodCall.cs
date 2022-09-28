// // Copyright 2022 Mindbox Ltd
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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
    internal sealed class MethodCall : IEquatable<MethodCall>
    {
		public string Name { get; }

	    private readonly IReadOnlyList<object> argumentValues;

	    public MethodCall(string name, IReadOnlyList<object> argumentValues)
	    {
		    Name = name;
		    this.argumentValues = argumentValues;
	    }

	    public IMethodCallDefinition ToMethodCallDefinition()
	    {
		    return new MethodCallDefinition(
			    Name,
			    argumentValues
				    .Select(argumentValue =>
						    new MethodArgumentDefinition(
							    TypeDefinition.GetTypeDefinitionByRuntimeType(argumentValue.GetType()),
							    argumentValue))
				    .ToArray());
	    }

	    public bool Equals(MethodCall other)
	    {
		    if (other == null)
			    return false;
		    if (ReferenceEquals(this, other))
			    return true;

		    if (!StringComparer.OrdinalIgnoreCase.Equals(Name, other.Name))
			    return false;

		    if (argumentValues.Count != other.argumentValues.Count)
			    return false;

		    for (int i = 0; i < argumentValues.Count; i++)
		    {
			    var thisValue = argumentValues[i];
				var otherValue = other.argumentValues[i];

				// Messy, probably should rework this logic

			    if (thisValue is string thisStringValue && otherValue is string otherStringValue)
			    {
				    if (!StringComparer.OrdinalIgnoreCase.Equals(thisStringValue, otherStringValue))
					    return false;
			    }
				else if (!argumentValues[i].Equals(other.argumentValues[i]))
				    return false;
		    }

		    return true;
	    }

	    public override bool Equals(object obj)
	    {
		    if (ReferenceEquals(null, obj))
			    return false;
		    if (ReferenceEquals(this, obj))
			    return true;

		    return Equals((MethodCall)obj);
	    }

	    public override int GetHashCode()
	    {
			return StringComparer.OrdinalIgnoreCase.GetHashCode(Name);
		}

	    public override string ToString()
	    {
		    return $"{Name}({string.Join(", ", argumentValues)})";
	    }
    }
}
