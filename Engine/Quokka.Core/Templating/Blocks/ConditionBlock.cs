using System.Text;

namespace Mindbox.Quokka
{
	internal class ConditionBlock : TemplateNodeBase
	{
		private readonly BooleanExpression conditionExpression;
		private readonly ITemplateNode block;
		
		public ConditionBlock(BooleanExpression conditionExpression, ITemplateNode block)
		{
			this.block = block;
			this.conditionExpression = conditionExpression;
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			conditionExpression.PerformSemanticAnalysis(context);
			block?.PerformSemanticAnalysis(context);
		}

		public override void Render(StringBuilder resultBuilder, RenderContext renderContext)
		{
			block?.Render(resultBuilder, renderContext);
		}

		public override void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context)
		{
			block?.CompileGrammarSpecificData(context);
		}

		public bool ShouldRender(RenderContext renderContext)
		{
			return conditionExpression.GetBooleanValue(renderContext);
		}
	}
}
