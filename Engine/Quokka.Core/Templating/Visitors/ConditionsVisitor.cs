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

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class ConditionsVisitor : QuokkaBaseVisitor<ConditionBlock>
	{
		public ConditionsVisitor(VisitingContext visitingContext)
			: base(visitingContext)
		{
		}

		public override ConditionBlock VisitIfCondition(QuokkaParser.IfConditionContext context)
		{
			return new ConditionBlock(
				context.ifInstruction().booleanExpression().Accept(new BooleanExpressionVisitor(VisitingContext)),
				context.templateBlock()?.Accept(new TemplateVisitor(VisitingContext)));
		}

		public override ConditionBlock VisitElseIfCondition(QuokkaParser.ElseIfConditionContext context)
		{
			return new ConditionBlock(
				context.elseIfInstruction().booleanExpression().Accept(new BooleanExpressionVisitor(VisitingContext)),
				context.templateBlock()?.Accept(new TemplateVisitor(VisitingContext)));
		}

		public override ConditionBlock VisitElseCondition(QuokkaParser.ElseConditionContext context)
		{
			return new ConditionBlock(
				new TrueExpression(),
				context.templateBlock()?.Accept(new TemplateVisitor(VisitingContext)));
		}
	}
}
