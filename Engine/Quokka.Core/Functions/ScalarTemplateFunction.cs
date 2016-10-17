using System;
using System.Collections.Generic;

namespace Quokka
{
	public abstract class ScalarTemplateFunction : TemplateFunction
	{
		protected ScalarTemplateFunction(
			string name,
			Type returnType,
			params TemplateFunctionArgument[] arguments)
			: base(
				  name,
				  new PrimitiveModelDefinition(TypeDefinition.GetTypeDefinitionByRuntimeType(returnType)),
				  arguments)
		{
		}

		protected internal ScalarTemplateFunction(
			string name,
			Type returnType,
			Func<TemplateFunction, IEnumerable<TemplateFunctionArgument>, ArgumentList> argumentListFactory,
			params TemplateFunctionArgument[] arguments)
			: base(
				  name,
				  new PrimitiveModelDefinition(TypeDefinition.GetTypeDefinitionByRuntimeType(returnType)),
				  argumentListFactory,
				  arguments)
		{
		}

		internal abstract object GetScalarInvocationResult(IList<VariableValueStorage> argumentsValues);

		internal override VariableValueStorage Invoke(IList<VariableValueStorage> argumentsValues)
		{
			return new PrimitiveVariableValueStorage(GetScalarInvocationResult(argumentsValues));
		}
	}
}
