using System.Text;

namespace Quokka
{
	internal class VariableOutputBlock : TemplateNodeBase, IOutputBlock
	{
		private readonly VariableOccurence variableOccurence;

		public VariableOutputBlock(VariableOccurence variableOccurence)
		{
			this.variableOccurence = variableOccurence;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			context.VariableScope.CreateOrUpdateVariableDefinition(variableOccurence);
		}

		public override void Render(StringBuilder resultBuilder, RenderContext context)
		{
			resultBuilder.Append(context.VariableScope.GetVariableValue(variableOccurence));
		}
	}
}
