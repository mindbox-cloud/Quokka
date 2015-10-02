using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quokka
{
	internal class ArithmeticExpressionOutputBlock : TemplateNodeBase
	{
		private readonly IArithmeticExpression expression;

		public ArithmeticExpressionOutputBlock(IArithmeticExpression expression)
		{
			this.expression = expression;
		}
	}
}
