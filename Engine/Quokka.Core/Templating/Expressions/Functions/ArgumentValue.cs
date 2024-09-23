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
	internal class ArgumentValue
	{
		public IExpression Expression { get; }

		public Location Location { get; }

		public ArgumentValue(IExpression expression, Location location)
		{
			Expression = expression;
			Location = location;
		}

		public void PerformSemanticAnalysis(AnalysisContext context, TypeDefinition requiredArgumentType)
		{
			Expression.PerformSemanticAnalysis(context, requiredArgumentType);
		}

		public TypeDefinition GetStaticType(AnalysisContext context)
		{
			return Expression.GetResultType(context);
		}

		public VariableValueStorage GetValue(RenderContext renderContext, TemplateFunctionArgument templateFunctionArgument)
		{
			if (templateFunctionArgument.AllowsNull && Expression.CheckIfExpressionIsNull(renderContext))
				return new PrimitiveVariableValueStorage(new PrimitiveModelValue(null));
			return Expression.Evaluate(renderContext);
		}

		public VariableValueStorage TryGetStaticValue()
		{
			return Expression.TryGetStaticEvaluationResult();
		}

		public ExpressionDTO GetTreeDTO()
		{
			var dto = new ExpressionDTO();
			dto.type = "ArgumentValue";
			dto.argumentExpression = Expression.GetTreeDTO();
			return dto;
		}
	}
}
