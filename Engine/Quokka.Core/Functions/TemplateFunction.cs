using System;
using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	public abstract class TemplateFunction
	{
		public IReadOnlyList<TemplateFunctionArgument> Arguments { get; }
		public IModelDefinition ReturnValueDefinition { get; }
		public string Name { get; }

		protected TemplateFunction(
			string name,
			IModelDefinition returnValueDefinition,
			params TemplateFunctionArgument[] arguments)
		{
			Name = name;
			ReturnValueDefinition = returnValueDefinition;
			Arguments = arguments.ToList().AsReadOnly();
		}
		
		internal abstract VariableValueStorage Invoke(IList<VariableValueStorage> argumentsValues);
	}}