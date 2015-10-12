using System;
using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	public abstract class TemplateFunction
	{
		private readonly IReadOnlyList<Type> argumentTypes;
		public string Name { get; }

		protected TemplateFunction(string name, Type returnType, params Type[] argumentTypes)
		{
			Name = name;
			this.argumentTypes = argumentTypes.ToList().AsReadOnly();
		}

		internal Type GetArgumentType(int index)
		{
			if (index < 0 || index >= argumentTypes.Count)
				throw new IndexOutOfRangeException("Index out of range");

			return argumentTypes[index];
		}

		internal abstract object Invoke(IList<object> arguments);
	}}