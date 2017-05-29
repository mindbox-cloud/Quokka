using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
	public interface IMethodCallDefinition
	{
		string Name { get; }
		IReadOnlyList<IMethodArgumentDefinition> Arguments { get; }
	}

	public class MethodCallDefinition : IMethodCallDefinition
	{
		public string Name { get; }

		public IReadOnlyList<IMethodArgumentDefinition> Arguments { get; }

	    internal MethodCallDefinition(string name, IReadOnlyList<IMethodArgumentDefinition> arguments)
	    {
		    Name = name;
		    Arguments = arguments;
	    }
    }

	public interface IMethodArgumentDefinition
	{
		TypeDefinition Type { get; }
		object Value { get; }
	}

	public class MethodArgumentDefinition : IMethodArgumentDefinition
	{
		public TypeDefinition Type { get; }
		public object Value { get; }

		internal MethodArgumentDefinition(TypeDefinition type, object value)
		{
			Type = type;
			Value = value;
		}
	}
}
