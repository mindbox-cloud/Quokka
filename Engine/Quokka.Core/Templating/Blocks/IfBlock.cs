using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mindbox.Quokka
{
	internal class IfBlock : TemplateNodeBase
	{
		private readonly IReadOnlyCollection<ConditionBlock> conditions;

		public IfBlock(IEnumerable<ConditionBlock> conditions)
		{
			this.conditions = conditions.ToList().AsReadOnly();
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			foreach (var condition in conditions)
				condition.PerformSemanticAnalysis(context);
		}

		public override void Render(TextWriter resultWriter, RenderContext renderContext)
		{
			foreach (var condition in conditions)
			{
				if (condition.ShouldRender(renderContext))
				{
					condition.Render(resultWriter, renderContext);
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
