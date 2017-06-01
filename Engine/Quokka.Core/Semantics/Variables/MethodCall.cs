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
