using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quokka
{
	internal class IfBlock : TemplateNodeBase
	{
		private readonly IReadOnlyCollection<ConditionBlock> conditions;

		public IfBlock(IEnumerable<ConditionBlock> conditions)
		{
			this.conditions = conditions.ToList().AsReadOnly();
		}

		public override void CompileVariableDefinitions(CompilationVariableScope scope)
		{
			foreach (var condition in conditions)
				condition.CompileVariableDefinitions(scope);
		}

		public override void Render(StringBuilder resultBuilder, RuntimeVariableScope variableScope)
		{
			foreach (var condition in conditions)
			{
				if (condition.ShouldRender(variableScope))
				{
					condition.Render(resultBuilder, variableScope);
					break;
				}
			}
		}
	}
}
