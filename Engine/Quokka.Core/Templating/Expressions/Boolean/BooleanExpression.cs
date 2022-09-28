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
	internal abstract class BooleanExpression : Expression
	{
		public abstract bool GetBooleanValue(RenderContext renderContext);

		public override TypeDefinition GetResultType(AnalysisContext context)
		{
			return TypeDefinition.Boolean;
		}

		public override VariableValueStorage Evaluate(RenderContext renderContext)
		{
			return new PrimitiveVariableValueStorage(GetBooleanValue(renderContext));
		}

		public sealed override void PerformSemanticAnalysis(AnalysisContext context, TypeDefinition expectedExpressionType)
		{
			PerformSemanticAnalysis(context);
		}

		public abstract void PerformSemanticAnalysis(AnalysisContext context);

		public override VariableValueStorage TryGetStaticEvaluationResult()
		{
			return null;
		}

		public sealed override void RegisterAssignmentToVariable(
			AnalysisContext context,
			ValueUsageSummary destinationVariable)
		{
			// do nothing
		}
	}
}
