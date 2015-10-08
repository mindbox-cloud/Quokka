using System.Text;

namespace Quokka
{
	internal class VariableOutputBlock : TemplateNodeBase
	{
		private readonly VariableOccurence variableOccurence;

		public VariableOutputBlock(VariableOccurence variableOccurence)
		{
			this.variableOccurence = variableOccurence;
		}

		public override void CompileVariableDefinitions(CompilationVariableScope scope)
		{
			scope.CreateOrUpdateVariableDefinition(variableOccurence);
		}

		public override void Render(StringBuilder resultBuilder, RuntimeVariableScope variableScope)
		{
			resultBuilder.Append(variableScope.GetVariableValue(variableOccurence));
		}
	}
}
