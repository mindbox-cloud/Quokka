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

namespace Mindbox.Quokka
{
	public abstract class ScalarTemplateFunction : TemplateFunction
	{
		protected ScalarTemplateFunction(
			string name,
			Type returnType,
			params TemplateFunctionArgument[] arguments)
			: base(
				  name,
				  new PrimitiveModelDefinition(TypeDefinition.GetTypeDefinitionByRuntimeType(returnType)),
				  arguments)
		{
		}

		protected internal ScalarTemplateFunction(
			string name,
			Type returnType,
			Func<TemplateFunction, IEnumerable<TemplateFunctionArgument>, ArgumentList> argumentListFactory,
			params TemplateFunctionArgument[] arguments)
			: base(
				  name,
				  new PrimitiveModelDefinition(TypeDefinition.GetTypeDefinitionByRuntimeType(returnType)),
				  argumentListFactory,
				  arguments)
		{
		}

		internal abstract object GetScalarInvocationResult(
			RenderContext renderContext,
			IReadOnlyList<VariableValueStorage> argumentsValues);

		internal sealed override VariableValueStorage Invoke(
			RenderContext renderContext,
			DeferredArguments deferredArguments)
		{
			var argumentsValues = new VariableValueStorage[deferredArguments.Count];
			for (var argumentNumber = 0; argumentNumber < argumentsValues.Length; argumentNumber++)
				argumentsValues[argumentNumber] = deferredArguments.Evaluate(argumentNumber);

			return new PrimitiveVariableValueStorage(GetScalarInvocationResult(renderContext, argumentsValues));
		}
	}
}
