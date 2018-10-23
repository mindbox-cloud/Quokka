using System;
using System.Collections.Generic;

namespace Mindbox.Quokka
{
	public abstract class ContextScalarTemplateFunction<TContext> : ScalarTemplateFunction
		where TContext : class
	{
		protected ContextScalarTemplateFunction(
			string name, Type returnType, params TemplateFunctionArgument[] arguments) : base(name, returnType, arguments)
		{
		}

		protected ContextScalarTemplateFunction(
			string name, 
			Type returnType,
			Func<TemplateFunction, IEnumerable<TemplateFunctionArgument>, ArgumentList> argumentListFactory, 
			params TemplateFunctionArgument[] arguments) : base(name, returnType, argumentListFactory, arguments)
		{
		}

		internal abstract object GetContextScalarInvocationResult(TContext context, IList<VariableValueStorage> argumentsValues);

		internal sealed override object GetScalarInvocationResult(
			RenderContext renderContext, 
			IList<VariableValueStorage> argumentsValues)
		{
			var contextValue = renderContext.GetCallContextValue<TContext>();

			return GetContextScalarInvocationResult(contextValue, argumentsValues);
		}
	}
}