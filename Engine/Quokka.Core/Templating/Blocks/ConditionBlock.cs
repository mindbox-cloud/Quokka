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

using System.IO;

namespace Mindbox.Quokka
{
	internal class ConditionBlock : TemplateNodeBase
	{
		private readonly BooleanExpression conditionExpression;
		private readonly ITemplateNode block;
		
		public ConditionBlock(BooleanExpression conditionExpression, ITemplateNode block)
		{
			this.block = block;
			this.conditionExpression = conditionExpression;
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			conditionExpression.PerformSemanticAnalysis(context);
			block?.PerformSemanticAnalysis(context);
		}

		public override void Render(TextWriter resultWriter, RenderContext renderContext)
		{
			block?.Render(resultWriter, renderContext);
		}

		public override void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context)
		{
			block?.CompileGrammarSpecificData(context);
		}

		public bool ShouldRender(RenderContext renderContext)
		{
			return conditionExpression.GetBooleanValue(renderContext);
		}
		
		public override void Accept(ITemplateVisitor treeVisitor)
		{
			treeVisitor.VisitConditionBlock();

			block?.Accept(treeVisitor);
			conditionExpression.Accept(treeVisitor);
			
			treeVisitor.EndVisit();
		}
	}
}
