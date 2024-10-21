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

namespace Mindbox.Quokka
{
	internal class NotExpression : BooleanExpression
	{
		private readonly BooleanExpression inner;

		public NotExpression(BooleanExpression inner)
		{
			this.inner = inner;
		}

		public override bool GetBooleanValue(RenderContext renderContext)
		{
			return !inner.GetBooleanValue(renderContext);
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			inner.PerformSemanticAnalysis(context);
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			return inner.CheckIfExpressionIsNull(renderContext);
		}

		public override void Accept(ITemplateVisitor treeVisitor)
		{
			treeVisitor.VisitNotExpression();

			inner.Accept(treeVisitor);
			
			treeVisitor.EndVisit();
		}
	}
}
