using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class EnumerableElementVisitor : QuokkaBaseVisitor<IEnumerableElement>
	{
		public EnumerableElementVisitor(VisitingContext visitingContext)
			: base(visitingContext)
		{
		}

		public override IEnumerableElement VisitFunctionCall(QuokkaParser.FunctionCallContext context)
		{
			return new FunctionValueEnumerableElement(
				context.Accept(new FunctionCallVisitor(visitingContext)));
		}

		public override IEnumerableElement VisitParameterValueExpression(QuokkaParser.ParameterValueExpressionContext context)
		{
			return new ParameterValueEnumerableElement(
				context.Accept(new VariableVisitor(visitingContext, TypeDefinition.Array)));
		}

		protected override IEnumerableElement AggregateResult(IEnumerableElement aggregate, IEnumerableElement nextResult)
		{
			return aggregate ?? nextResult;
		}
	}
}
