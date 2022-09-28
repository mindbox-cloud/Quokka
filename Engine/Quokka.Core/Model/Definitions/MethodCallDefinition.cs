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
