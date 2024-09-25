// // Copyright 2022 Mindbox Ltd
// //
// // Licensed under the Apache License, Version 2.0 (the "License");
// // you may not use this file except in compliance with the License.
// // You may obtain a copy of the License at
// //
// //     http://www.apache.org/licenses/LICENSE-2.0
// //
// // Unless required by applicable law or agreed to in writing, software
// // distributed under the License is distributed on an "AS IS" BASIS,
// // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// // See the License for the specific language governing permissions and
// // limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
	internal class MemberValueExpression : VariantValueExpression
	{
		private readonly VariableValueExpression ownerExpression;

		private readonly IReadOnlyList<Member> members;

		public MemberValueExpression(VariableValueExpression ownerExpression, IReadOnlyList<Member> members)
		{
			this.ownerExpression = ownerExpression;
			this.members = members;
		}

		public override void PerformSemanticAnalysis(AnalysisContext context, TypeDefinition expectedExpressionType)
		{
			ownerExpression.PerformSemanticAnalysis(context, TypeDefinition.Composite);

			var ownerVariableDefinition = ownerExpression.GetVariableDefinition(context);

			for (int i = 0; i < members.Count; i++)
			{
				bool isLastMember = i == members.Count - 1;

				var memberType = isLastMember
								? expectedExpressionType
								: TypeDefinition.Composite;

				members[i].PerformSemanticAnalysis(context, ownerVariableDefinition, memberType);

				if (!isLastMember)
					ownerVariableDefinition = members[i].GetMemberVariableDefinition(ownerVariableDefinition);
			}
		}

		public override void RegisterIterationOverExpressionResult(AnalysisContext context, ValueUsageSummary iterationVariable)
		{
			var leafMemberVariableDefinition = GetLeafMemberVariableDefinition(context);
			leafMemberVariableDefinition.AddEnumerationResultUsageSummary(iterationVariable);
		}

		public ValueUsageSummary GetLeafMemberVariableDefinition(AnalysisContext context)
		{
			var leafMemberVariableDefinition = ownerExpression.GetVariableDefinition(context);

			foreach (var member in members)
				leafMemberVariableDefinition = member.GetMemberVariableDefinition(leafMemberVariableDefinition);

			return leafMemberVariableDefinition;
		}

		public override IModelDefinition GetExpressionResultModelDefinition(AnalysisContext context)
		{
			return new PrimitiveModelDefinition(TypeDefinition.Unknown);
		}

		public override VariableValueStorage Evaluate(RenderContext renderContext)
		{
			var value = ownerExpression.Evaluate(renderContext);

			for (var i = 0; i < members.Count; i++)
			{
				value = members[i].GetMemberValue(value);

				if (value == null || value.CheckIfValueIsNull())
				{
					var location = members[i].Location;
					var memberChainStringRepresentation =
						string.Join(
							".",
							new[] { ownerExpression.StringRepresentation }
								.Concat(
									members
										.Select(m => m.StringRepresentation)
										.Take(i + 1)));

					throw new UnrenderableTemplateModelException(
						$"An attempt to use the value of \"{memberChainStringRepresentation}\" expression which happens to be null",
						location);
				}
			}

			return value;
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			if (ownerExpression.CheckIfExpressionIsNull(renderContext))
				return true;

			var value = ownerExpression.TryGetValueStorage(renderContext);

			foreach (Member member in members)
			{
				value = member.GetMemberValue(value);
				if (value == null || value.CheckIfValueIsNull())
					return true;
			}

			return false;
		}

		public sealed override void RegisterAssignmentToVariable(
			AnalysisContext context,
			ValueUsageSummary destinationVariable)
		{
			GetLeafMemberVariableDefinition(context).RegisterAssignmentToVariable(destinationVariable);
		}

		public override ExpressionDTO GetTreeDTO()
		{
			var dto = base.GetTreeDTO();
			dto.type = "MemberValueExpression";

			dto.variableName = ownerExpression.GetTreeDTO().variableName;
			dto.members = members.Select(m => m.GetTreeDTO()).ToList();
			return dto;
		}
	}
}
