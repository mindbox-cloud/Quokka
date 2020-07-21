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

	public class MethodCallDefinition : IMethodCallDefinition, IEquatable<IMethodCallDefinition>
	{
		public string Name { get; }

		public IReadOnlyList<IMethodArgumentDefinition> Arguments { get; }

	    internal MethodCallDefinition(string name, IReadOnlyList<IMethodArgumentDefinition> arguments)
	    {
		    Name = name;
		    Arguments = arguments;
	    }

		public bool Equals(IMethodCallDefinition other)
		{
			if (other == null)
				return false;

			if (!StringComparer.OrdinalIgnoreCase.Equals(Name, other.Name))
				return false;

			if (Arguments.Count != other.Arguments.Count)
				return false;

			for (int i = 0; i < Arguments.Count; i++)
			{
				if (Arguments[i].Type != other.Arguments[i].Type)
					return false;
				
				// Messy, probably should rework this logic
				if (Arguments[i].Value is string thisStringValue && other.Arguments[i].Value is string otherStringValue)
				{
					if (!StringComparer.OrdinalIgnoreCase.Equals(thisStringValue, otherStringValue))
						return false;
				}
				else if (!Arguments[i].Value.Equals(other.Arguments[i].Value))
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

			return Equals((IMethodCallDefinition)obj);
		}

		public override int GetHashCode()
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(Name);
		}

		public override string ToString() => 
			$"{Name}({string.Join(", ", Arguments.Select(arg => $"{arg.Type.Name}: {arg.Value}"))})";
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
