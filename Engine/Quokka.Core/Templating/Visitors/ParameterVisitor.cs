using Quokka.Generated;

namespace Quokka
{
	internal class ParameterVisitor : QuokkaBaseVisitor<ParameterMetadata>
	{
		private readonly ParameterType requiredParameterExpressionType;

		public ParameterVisitor(ParameterType requiredParameterExpressionType)
		{
			this.requiredParameterExpressionType = requiredParameterExpressionType;
		}

		public override ParameterMetadata VisitParameterValueExpression(QuokkaParser.ParameterValueExpressionContext context)
		{
			var member = context.memberAccessExpression()?.Accept(this);

			return new ParameterMetadata(
				context.parameterExpression().Identifier().GetText(),
				member == null
					? requiredParameterExpressionType
					: ParameterType.Composite,
				member);
		}

		public override ParameterMetadata VisitMemberAccessExpression(QuokkaParser.MemberAccessExpressionContext context)
		{
			var subMember = context.memberAccessExpression()?.Accept(this);
			return new ParameterMetadata(
				context.Identifier().GetText(),
				subMember == null 
					? requiredParameterExpressionType
					: ParameterType.Composite,
				subMember);
		}
	}
}
