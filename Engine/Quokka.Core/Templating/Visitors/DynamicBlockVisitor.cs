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

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class DynamicBlockVisitor : QuokkaBaseVisitor<ITemplateNode>
	{
		public DynamicBlockVisitor(VisitingContext visitingContext) 
			: base(visitingContext)
		{
		}

		public override ITemplateNode VisitIfStatement(QuokkaParser.IfStatementContext context)
		{
			var conditionsVisitor = new ConditionsVisitor(VisitingContext);
			var conditions = new List<ConditionBlock>
			{
				context.ifCondition().Accept(conditionsVisitor)
			};
			conditions.AddRange(context.elseIfCondition()
				.Select(elseIf => elseIf.Accept(conditionsVisitor)));
			if (context.elseCondition() != null)
				conditions.Add(context.elseCondition().Accept(conditionsVisitor));

			return new IfBlock(conditions);
		}

		public override ITemplateNode VisitForStatement(QuokkaParser.ForStatementContext context)
		{
			var forInstruction = context.forInstruction();

			var iterationVariableIdentifier = forInstruction.iterationVariable().Identifier();

			return new ForBlock(
				context.templateBlock()?.Accept(new TemplateVisitor(VisitingContext)),
				iterationVariableIdentifier.GetText(),
				GetLocationFromToken(iterationVariableIdentifier.Symbol),
				forInstruction.variantValueExpression().Accept(new VariantValueExpressionVisitor(VisitingContext)));
		}

		public override ITemplateNode VisitCommentBlock(QuokkaParser.CommentBlockContext context)
		{
			return null;
		}

		public override ITemplateNode VisitAssignmentBlock(QuokkaParser.AssignmentBlockContext context)
		{
			return new AssignmentBlock(
				context.Identifier().GetText(),
				context.expression().Accept(new ExpressionVisitor(VisitingContext)),
				GetLocationFromToken(context.Identifier().Symbol));
		}
	}
}
