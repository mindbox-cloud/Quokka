using System;
using System.Collections.Generic;

namespace Mindbox.Quokka
{
	public abstract class TemplateFunction
	{
		internal ArgumentList Arguments { get; }
		public IModelDefinition ReturnValueDefinition { get; }
		public string Name { get; }

		protected TemplateFunction(
			string name,
			IModelDefinition returnValueDefinition,
			Func<TemplateFunction, IEnumerable<TemplateFunctionArgument>, ArgumentList> argumentListFactory, 
			params TemplateFunctionArgument[] arguments)
		{
			Name = name;
			ReturnValueDefinition = returnValueDefinition;
			Arguments = argumentListFactory(this, arguments);
		}

		protected TemplateFunction(
			string name,
			IModelDefinition returnValueDefinition,
			params TemplateFunctionArgument[] arguments) : 
			this(name, 
				returnValueDefinition, 
				(function, functionArguments) => new ArgumentList(function, functionArguments), 
				arguments)
		{
		}

		internal abstract VariableValueStorage Invoke(IList<VariableValueStorage> argumentsValues);

		internal bool Accepts(IReadOnlyList<ArgumentValue> arguments)
		{
			// only overload by arguments count is currently supported
			return Arguments.CheckArgumentNumber(arguments);
		}
	}
}