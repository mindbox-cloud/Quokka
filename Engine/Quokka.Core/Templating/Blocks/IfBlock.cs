using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindbox.Quokka
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

		public override void Render(StringBuilder resultBuilder, RenderContext renderContext)
		{
			foreach (var condition in conditions)
			{
				if (condition.ShouldRender(renderContext))
				{
					condition.Render(resultBuilder, renderContext);
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
