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

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			foreach (var condition in conditions)
				condition.CompileVariableDefinitions(context);
		}

		public override void Render(StringBuilder resultBuilder, RenderContext context)
		{
			foreach (var condition in conditions)
			{
				if (condition.ShouldRender(context))
				{
					condition.Render(resultBuilder, context);
					break;
				}
			}
		}

		public override void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context)
		{
			foreach (var condition in conditions)
				condition.CompileGrammarSpecificData(context);
		}
	}
}
