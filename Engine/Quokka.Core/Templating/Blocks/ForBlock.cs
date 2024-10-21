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
using System.IO;
using System.Text;

namespace Mindbox.Quokka
{
	internal class ForBlock : TemplateNodeBase
	{
		private readonly ITemplateNode block;
		private readonly string iterationVariableName;
		private readonly Location iterationVariableLocation;
		private readonly VariantValueExpression enumerableExpression;

		public ForBlock(
			ITemplateNode block,
			string iterationVariableName, 
			Location iterationVariableLocation,
			VariantValueExpression enumerableExpression)
		{
			this.block = block;
			this.iterationVariableName = iterationVariableName;
			this.iterationVariableLocation = iterationVariableLocation;
			this.enumerableExpression = enumerableExpression;
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			var innerSemanticContext = context.CreateNestedScopeContext();
			var iterationVariableDefinition = innerSemanticContext.VariableScope.DeclareVariable(
				iterationVariableName,
				new ValueUsage(iterationVariableLocation, TypeDefinition.Unknown));

			enumerableExpression.PerformSemanticAnalysis(context, TypeDefinition.Array);
			block?.PerformSemanticAnalysis(innerSemanticContext);
			enumerableExpression.RegisterIterationOverExpressionResult(context, iterationVariableDefinition);

			var enumerableElementModelDefinition =
				enumerableExpression.GetExpressionResultModelDefinition(context) is IArrayModelDefinition arrayModelDefinition
					? arrayModelDefinition.ElementModelDefinition
					: new PrimitiveModelDefinition(TypeDefinition.Unknown);

			iterationVariableDefinition.ValidateAgainstExpectedModelDefinition(
				enumerableElementModelDefinition,
				context.ErrorListener);
		}

		public override void Render(TextWriter resultWriter, RenderContext renderContext)
		{
			if (block == null)
				return;

			var collectionValue = enumerableExpression.Evaluate(renderContext).GetElements();

			foreach (var collectionElement in collectionValue)
			{
				var innerScope =
					renderContext.VariableScope.CreateChildScope(
						new CompositeVariableValueStorage(iterationVariableName, collectionElement));

				block.Render(resultWriter, renderContext.CreateInnerContext(innerScope));
			}
		}

		public override void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context)
		{
			block?.CompileGrammarSpecificData(context);
		}

		public override void Accept(ITreeVisitor treeVisitor)
		{
			treeVisitor.VisitForBlock(iterationVariableName);
			
			block?.Accept(treeVisitor);
			enumerableExpression.Accept(treeVisitor);
			
			treeVisitor.EndVisit();
		}
	}
}
