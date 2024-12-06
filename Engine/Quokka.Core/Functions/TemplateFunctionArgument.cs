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

namespace Mindbox.Quokka
{
	public abstract class TemplateFunctionArgument
	{
		public string Name { get; }
		internal abstract TypeDefinition Type { get; }

		internal bool AllowsNull { get; }

		protected TemplateFunctionArgument(string name, bool allowsNull = false)
		{
			ArgumentException.ThrowIfNullOrEmpty(name);

			Name = name;
			AllowsNull = allowsNull;
		}

		internal abstract ArgumentValueValidationResult ValidateConstantValue(VariableValueStorage value);

		/// <summary>
		/// Performs additional semantic analysis on expressions used argument values based on usages
		/// of function result.
		/// Some function return a "projection" of its argument (array or other value) meaning that the subsequent usages of
		/// function result should be considered when determining the exact type of said array or other value.
		/// </summary>
		/// <remarks>
		/// This mechanism isn't available for third-party functions. Functions that use this mechanism
		/// are designed with strong understanding of implementation details of the templating process.
		/// </remarks>
		internal virtual void AnalyzeArgumentValueBasedOnFunctionResultUsages(
			AnalysisContext context,
			ValueUsageSummary resultValueUsageSummary,
			IExpression argumentValueExpression)
		{
		}
	}
}