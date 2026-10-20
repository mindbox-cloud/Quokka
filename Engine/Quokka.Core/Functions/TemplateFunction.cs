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
	public abstract class TemplateFunction
	{
		internal ArgumentList Arguments { get; }
		public IModelDefinition ReturnValueDefinition { get; }
		public string Name { get; }

		protected TemplateFunction(
			string name,
			IModelDefinition returnValueDefinition,
			Func<TemplateFunction, IEnumerable<TemplateFunctionArgument>, ArgumentList> argumentListFactory, 
			params TemplateFunctionArgument[] arguments)
		{
			Name = name;
			ReturnValueDefinition = returnValueDefinition;
			Arguments = argumentListFactory(this, arguments);
		}

		protected TemplateFunction(
			string name,
			IModelDefinition returnValueDefinition,
			params TemplateFunctionArgument[] arguments) : 
			this(name, 
				returnValueDefinition, 
				(function, functionArguments) => new ArgumentList(function, functionArguments), 
				arguments)
		{
		}

		/// <summary>
		/// Invokes the function. The function decides which of its arguments are evaluated
		/// and evaluates them with <see cref="DeferredArguments.Evaluate"/> during the invocation.
		/// An exception thrown by <see cref="DeferredArguments.Evaluate"/> is an error of the argument expression:
		/// it is reported with the expression and location of the argument only if it leaves the invocation as is,
		/// an exception that replaces it is reported as an error of the function.
		/// Each call of <see cref="DeferredArguments.Evaluate"/> evaluates the argument again.
		/// </summary>
		internal abstract VariableValueStorage Invoke(
			RenderContext renderContext,
			DeferredArguments deferredArguments);

		internal bool Accepts(IReadOnlyList<ArgumentValue> arguments)
		{
			// only overload by arguments count is currently supported
			return Arguments.CheckArgumentNumber(arguments);
		}
	}
}