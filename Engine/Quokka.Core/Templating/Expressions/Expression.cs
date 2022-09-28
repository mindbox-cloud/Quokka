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
	internal abstract class Expression : IExpression
	{
		public abstract VariableValueStorage TryGetStaticEvaluationResult();

		public abstract VariableValueStorage Evaluate(RenderContext renderContext);

		public abstract TypeDefinition GetResultType(AnalysisContext context);

		public abstract void PerformSemanticAnalysis(AnalysisContext context, TypeDefinition expectedExpressionType);

		public abstract void RegisterAssignmentToVariable(
			AnalysisContext context, 
			ValueUsageSummary destinationVariable);

		public abstract bool CheckIfExpressionIsNull(RenderContext renderContext);

		public virtual string GetOutputValue(RenderContext renderContext)
		{
			return Evaluate(renderContext).GetPrimitiveValue().ToString();
		}
	}
}
