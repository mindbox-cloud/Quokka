using Quokka.Generated;

namespace Quokka
{
	internal class VariableVisitor : QuokkaBaseVisitor<VariableOccurence>
	{
		private readonly TypeDefinition requiredVariableExpressionType;

		public VariableVisitor(
			VisitingContext visitingContext,
			TypeDefinition requiredVariableExpressionType)
			: base(visitingContext)
		{
			this.requiredVariableExpressionType = requiredVariableExpressionType;
		}

		public override VariableOccurence VisitParameterValueExpression(QuokkaParser.ParameterValueExpressionContext context)
		{
			var member = context.memberAccessExpression()?.Accept(this);

			var identifier = context.parameterExpression().Identifier();
			return new VariableOccurence(
				identifier.GetText(),
				GetLocationFromToken(identifier.Symbol),
				member == null
					? requiredVariableExpressionType
					: TypeDefinition.Composite,
				member);
		}

		public override VariableOccurence VisitMemberAccessExpression(QuokkaParser.MemberAccessExpressionContext context)
		{
			var subMember = context.memberAccessExpression()?.Accept(this);
			var identifier = context.Identifier();

			return new VariableOccurence(
				identifier.GetText(),
				GetLocationFromToken(identifier.Symbol),
                subMember == null 
					? requiredVariableExpressionType
					: TypeDefinition.Composite,
				subMember);
		}
	}
}
