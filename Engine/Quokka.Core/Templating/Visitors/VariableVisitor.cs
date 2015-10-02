using Quokka.Generated;

namespace Quokka
{
	internal class VariableVisitor : QuokkaBaseVisitor<VariableOccurence>
	{
		private readonly VariableType requiredVariableExpressionType;

		public VariableVisitor(VariableType requiredVariableExpressionType)
		{
			this.requiredVariableExpressionType = requiredVariableExpressionType;
		}

		public override VariableOccurence VisitParameterValueExpression(QuokkaParser.ParameterValueExpressionContext context)
		{
			var member = context.memberAccessExpression()?.Accept(this);

			return new VariableOccurence(
				context.parameterExpression().Identifier().GetText(),
				member == null
					? requiredVariableExpressionType
					: VariableType.Composite,
				member);
		}

		public override VariableOccurence VisitMemberAccessExpression(QuokkaParser.MemberAccessExpressionContext context)
		{
			var subMember = context.memberAccessExpression()?.Accept(this);
			return new VariableOccurence(
				context.Identifier().GetText(),
				subMember == null 
					? requiredVariableExpressionType
					: VariableType.Composite,
				subMember);
		}
	}
}
