using System;
using System.Text;

namespace Mindbox.Quokka
{
	internal class OutputInstructionBlock : TemplateNodeBase, IStaticBlockPart
	{
		private readonly IExpression expression;
		
		public int Offset { get; }
		public int Length { get; }

		public OutputInstructionBlock(IExpression expression, int offset, int length)
		{
			this.expression = expression;
			Offset = offset;
			Length = length;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			expression.CompileVariableDefinitions(context, TypeDefinition.Primitive);
		}

		public override void Render(StringBuilder resultBuilder, RenderContext renderContext)
		{
			resultBuilder.Append(expression.GetOutputValue(renderContext));
		}
	}
}
