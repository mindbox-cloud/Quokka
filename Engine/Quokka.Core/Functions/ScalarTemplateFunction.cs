using System;

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
	}
}
