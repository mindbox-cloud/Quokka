using System;

namespace Mindbox.Quokka
{
	public abstract class VariadicScalarTemplateFunction<TType> : ScalarTemplateFunction
	{
		protected VariadicArgument<TType> VariadicArgument { get; }

		protected VariadicScalarTemplateFunction(
			string name, 
			Type returnType, 
			VariadicArgument<TType> variadicArgument,
			params TemplateFunctionArgument[] arguments) 
			: base(
				name, 
				returnType, 
				(function, functionArguments) => new VariadicArgumentList<TType>(function, variadicArgument, functionArguments),
				arguments)
		{
			VariadicArgument = variadicArgument;
		}
	}
}