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
	internal class AnalysisContext
	{
		private readonly HashSet<string> nonIdempotentMethodNames;

		public CompilationVariableScope VariableScope { get; }
		public FunctionRegistry Functions { get; }
		public ISemanticErrorListener ErrorListener { get; }

		public AnalysisContext(
			CompilationVariableScope variableScope,
			FunctionRegistry functions,
			ISemanticErrorListener errorListener,
			IEnumerable<string> nonIdempotentMethodNames = null)
			: this(
				variableScope,
				functions,
				errorListener,
				new HashSet<string>(
					nonIdempotentMethodNames ?? Array.Empty<string>(),
					StringComparer.OrdinalIgnoreCase))
		{
		}

		private AnalysisContext(
			CompilationVariableScope variableScope,
			FunctionRegistry functions,
			ISemanticErrorListener errorListener,
			HashSet<string> nonIdempotentMethodNames)
		{
			VariableScope = variableScope;
			Functions = functions;
			ErrorListener = errorListener;
			this.nonIdempotentMethodNames = nonIdempotentMethodNames;
		}

		public bool IsMethodNonIdempotent(string methodName)
		{
			return nonIdempotentMethodNames.Contains(methodName);
		}

		public AnalysisContext CreateNestedScopeContext()
		{
			return new AnalysisContext(
				VariableScope.CreateChildScope(),
				Functions,
				ErrorListener,
				nonIdempotentMethodNames);
		}
	}
}
