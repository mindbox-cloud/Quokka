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

namespace Mindbox.Quokka
{
	internal class FunctionCallExpression : VariantValueExpression
	{
		public string FunctionName { get; }

		public Location Location { get; }

		private readonly IReadOnlyList<ArgumentValue> argumentValues;

		public FunctionCallExpression(string functionName, IEnumerable<ArgumentValue> argumentValues, Location location)
		{
			FunctionName = functionName;
			this.argumentValues = argumentValues.ToList().AsReadOnly();
			Location = location;
		}

		public override void PerformSemanticAnalysis(AnalysisContext context, TypeDefinition expectedExpressionType)
		{
			var resultType = GetResultType(context);
			if (!resultType.IsAssignableTo(expectedExpressionType))
				context.ErrorListener.AddInvalidFunctionResultTypeError(
					FunctionName,
					expectedExpressionType,
					resultType,
					Location);

			context.Functions.PerformSemanticAnalysis(context, FunctionName, argumentValues, Location);
		}
		
		public override VariableValueStorage Evaluate(RenderContext renderContext)
		{
			var function = renderContext.Functions.TryGetFunction(FunctionName, argumentValues);
			if (function == null)
				throw new InvalidOperationException($"Function {FunctionName} not found");

			try
			{
				return function.Invoke(
					renderContext,
					argumentValues
						.Select((argumentValue, argumentNumber) => 
							argumentValue.GetValue(renderContext, function.Arguments.GetArgument(argumentNumber)))
						.ToList());
			}
			catch (FunctionCallRuntimeException targetException)
			{
				throw new UnrenderableTemplateModelException(targetException.Message, targetException, Location);
			}
			catch (Exception ex)
			{
				throw new UnrenderableTemplateModelException(
					$"Function {FunctionName} invocation resulted in error",
					ex,
					Location);
			}
		}

		public override void RegisterIterationOverExpressionResult(AnalysisContext context, ValueUsageSummary iterationVariable)
		{
			var function = context.Functions.TryGetFunction(FunctionName, argumentValues);

			// We only do this if the template is semantically valid. If not, semantic errors are already handled
			// earlier in the workflow.
			function?.Arguments.AnalyzeArgumentValuesBasedOnFunctionResultUsages(context, argumentValues, iterationVariable);
		}

		public override IModelDefinition GetExpressionResultModelDefinition(AnalysisContext context)
		{
			var function = context.Functions.TryGetFunction(FunctionName, argumentValues);
			if (function == null)
				return new PrimitiveModelDefinition(TypeDefinition.Unknown);

			return function.ReturnValueDefinition;
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			var evaluationResult = Evaluate(renderContext);
			return evaluationResult.CheckIfValueIsNull();
		}

		public override void Accept(ITemplateVisitor treeVisitor)
		{
			treeVisitor.VisitFunctionCallExpression(FunctionName);

			foreach (var argumentValue in argumentValues)
				argumentValue.Accept(treeVisitor);
			
			treeVisitor.EndVisit();
		}

		public sealed override void RegisterAssignmentToVariable(
			AnalysisContext context,
			ValueUsageSummary destinationVariable)
		{
			// do nothing
		}
	}
}
