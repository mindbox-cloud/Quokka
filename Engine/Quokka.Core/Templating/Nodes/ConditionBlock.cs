using System.Text;

namespace Quokka
{
	internal class ConditionBlock : TemplateNodeBase
	{
		private readonly IBooleanExpression condition;
		private readonly ITemplateNode block;
		
		public ConditionBlock(IBooleanExpression condition, ITemplateNode block)
		{
			this.block = block;
			this.condition = condition;
		}

		public override void CompileVariableDefinitions(CompilationVariableScope scope)
		{
			condition.CompileVariableDefinitions(scope);
			block?.CompileVariableDefinitions(scope);
		}

		public override void Render(StringBuilder resultBuilder, RuntimeVariableScope variableScope)
		{
			block?.Render(resultBuilder, variableScope);
		}

		public bool ShouldRender(RuntimeVariableScope variableScope)
		{
			return condition.Evaluate(variableScope);
		}
	}
}
