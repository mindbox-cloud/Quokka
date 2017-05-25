using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	internal class AdditionExpression : ArithmeticExpressionBase
	{
		private readonly IReadOnlyCollection<AdditionOperand> operands;

		public override TypeDefinition Type
		{
			get
			{
				return operands.All(op => op.Expression.Type == TypeDefinition.Integer)
					? TypeDefinition.Integer
					: TypeDefinition.Decimal;
			}
		}

		public AdditionExpression(IEnumerable<AdditionOperand> operands)
		{
			this.operands = operands.ToList().AsReadOnly();
		}

		public override double GetValue(RenderContext renderContext)
		{
			return operands
				.Aggregate(0.0, (current, operand) => operand.Calculate(current, renderContext));
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			foreach (var operand in operands)
				operand.Expression.CompileVariableDefinitions(context);
		}
	}
}
