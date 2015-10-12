using System;
using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	public abstract class TemplateFunction
	{
		public IReadOnlyList<TemplateFunctionArgument> Arguments { get; }
		public Type ReturnType { get; }
		public string Name { get; }

		protected TemplateFunction(string name, Type returnType, params TemplateFunctionArgument[] arguments)
		{
			Name = name;
			ReturnType = returnType;
			Arguments = arguments.ToList().AsReadOnly();
		}
		
		internal abstract object Invoke(IList<object> arguments);
	}}