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
	internal abstract class VariantValueExpression : Expression
	{
		/// <summary>
		/// Collect all the semantic information about iterating over the expression result. All the information about the type
		/// of iteration variable will be used to compile the type of collection that is the result of the expression. 
		/// </summary>
		public abstract void RegisterIterationOverExpressionResult(AnalysisContext context, ValueUsageSummary iterationVariable);

		public abstract IModelDefinition GetExpressionResultModelDefinition(AnalysisContext context);
		
		public sealed override VariableValueStorage TryGetStaticEvaluationResult()
		{
			return null;
		}

		public sealed override TypeDefinition GetResultType(AnalysisContext context)
		{
			return TypeDefinition.GetTypeDefinitionFromModelDefinition(GetExpressionResultModelDefinition(context));
		}
	}
}