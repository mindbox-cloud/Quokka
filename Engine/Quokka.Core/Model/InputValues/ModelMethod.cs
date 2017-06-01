using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
    public class ModelMethod : IModelMethod
    {
		public string Name { get; }
	    public IReadOnlyList<object> Arguments { get; }
		public IModelValue Value { get; }

	    public ModelMethod(string name, IModelValue value)
			: this(name, Array.Empty<object>(), value)
	    {
	    }

	    public ModelMethod(string name, object primitiveValue)
		    : this(name, Array.Empty<object>(), primitiveValue)
	    {
	    }
		
	    public ModelMethod(string name, IEnumerable<object> arguments, IModelValue value)
	    {
		    Name = name;
		    Arguments = arguments.ToArray();
		    Value = value;
	    }

	    public ModelMethod(string name, IEnumerable<object> arguments, object primitiveValue)
		    : this(name, arguments, new PrimitiveModelValue(primitiveValue))
	    {
	    }
	}
}
