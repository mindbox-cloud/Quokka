using System.Collections.Generic;

namespace Quokka
{
	internal class VariadicArgumentList<TType> : ArgumentList
	{
		private readonly VariadicArgument<TType> variadicArgument;

		public VariadicArgumentList(
			TemplateFunction function, 
			VariadicArgument<TType> variadicArgument,
			IEnumerable<TemplateFunctionArgument> arguments) 
			: base(function, arguments)
		{
			this.variadicArgument = variadicArgument;
		}

		internal override bool CheckArgumentNumber(IReadOnlyList<IFunctionArgument> arguments)
		{
			return arguments.Count >= Arguments.Count;
		}

		protected override TemplateFunctionArgument GetArgument(int argumentNumber)
		{
			return argumentNumber < Arguments.Count ? Arguments[argumentNumber] : variadicArgument;
		}

		protected override TypeDefinition GetRequiredType(int argumentNumber)
		{
			return argumentNumber < Arguments.Count 
				? base.GetRequiredType(argumentNumber) 
				: variadicArgument.Type;
		}
	}
}