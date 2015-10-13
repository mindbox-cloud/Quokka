using System.Text;

namespace Quokka
{
	internal class BooleanExpressionOutputBlock : TemplateNodeBase, IOutputBlock
	{
		private const string TrueString = "True";
		private const string FalseString = "False";

		private readonly IBooleanExpression booleanExpression;

		public BooleanExpressionOutputBlock(IBooleanExpression booleanExpression)
		{
			this.booleanExpression = booleanExpression;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			booleanExpression.CompileVariableDefinitions(context);
		}

		public override void Render(StringBuilder resultBuilder, RenderContext context)
		{
			resultBuilder.Append(booleanExpression.Evaluate(context) ? TrueString : FalseString);
		}
	}
}
