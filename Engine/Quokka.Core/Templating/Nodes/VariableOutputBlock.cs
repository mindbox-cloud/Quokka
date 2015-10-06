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

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			scope.CreateOrUpdateVariableDefinition(variableOccurence, errorListener);
		}

		public override void Render(StringBuilder resultBuilder, VariableValueStorage valueStorage)
		{
			resultBuilder.Append(valueStorage.GetValue(variableOccurence));
		}
	}
}
