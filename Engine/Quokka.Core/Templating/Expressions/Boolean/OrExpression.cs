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

using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	internal class OrExpression : BooleanExpression
	{
		private readonly IReadOnlyCollection<BooleanExpression> subExpressions;

		public OrExpression(IEnumerable<BooleanExpression> subExpressions)
		{
			this.subExpressions = subExpressions.ToList().AsReadOnly();
		}

		public override bool GetBooleanValue(RenderContext renderContext)
		{
			// "Any" is guaranteed to short-circuit: 
			// "The enumeration of source is stopped as soon as the result can be determined."
			// https://msdn.microsoft.com/ru-ru/library/bb337697(v=vs.110).aspx

			return subExpressions.Any(subExpression => subExpression.GetBooleanValue(renderContext));
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			foreach (var subExpression in subExpressions)
				subExpression.PerformSemanticAnalysis(context);
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			return subExpressions.Any(expression => expression.CheckIfExpressionIsNull(renderContext));
		}

		public override void Accept(ITemplateVisitor treeVisitor)
		{
			treeVisitor.VisitOrExpression();

			foreach (var subExpression in subExpressions)
				subExpression.Accept(treeVisitor);
			
			treeVisitor.EndVisit();
		}
	}
}
