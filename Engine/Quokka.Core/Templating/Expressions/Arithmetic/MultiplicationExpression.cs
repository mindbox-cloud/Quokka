﻿using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	internal class MultiplicationExpression : ArithmeticExpression
	{
		private readonly IReadOnlyCollection<MultiplicationOperand> operands;
		
		public override TypeDefinition Type
		{
			get
			{
				return operands.All(op => op.Expression.Type == TypeDefinition.Integer)
					? TypeDefinition.Integer
					: TypeDefinition.Decimal;
			}
		}

		public MultiplicationExpression(IEnumerable<MultiplicationOperand> operands)
		{
			this.operands = operands.ToList().AsReadOnly();
		}

		public override double GetValue(RenderContext renderContext)
		{
			return operands
				.Aggregate(1.0, (current, operand) => operand.Calculate(current, renderContext));
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			foreach (var operand in operands)
				operand.Expression.PerformSemanticAnalysis(context);
		}
	}
}
