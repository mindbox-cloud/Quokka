using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			    if (!argumentValues[i].Equals(other.argumentValues[i]))
				    return false;

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
    }
}
