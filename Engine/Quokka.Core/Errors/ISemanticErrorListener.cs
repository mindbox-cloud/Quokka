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
	internal interface ISemanticErrorListener : IErrorListener
	{
		TSemanticErrorSubListener GetRegisteredSubListener<TSemanticErrorSubListener>()
			where TSemanticErrorSubListener : SemanticErrorSubListenerBase;

		void AddInconsistentVariableTypingError(
			ValueUsageSummary definition,
			ValueUsage faultyOccurence,
			TypeDefinition correctType);

		void AddUndefinedFunctionError(
			string functionName,
			Location location);

		void AddInvalidFunctionArgumentTypeError(
			string functionName,
			string argumentName,
			TypeDefinition realType,
			TypeDefinition expectedType,
			Location location);

		void AddInvalidFunctionArgumentValueError(
			string functionName,
			string argumentName,
			string message,
			Location location);

		void AddInvalidFunctionResultTypeError(
			string functionName,
			TypeDefinition expectedType,
			TypeDefinition realType,
			Location location);

		void AddInvalidFunctionArgumentCountError(
			string functionName,
			int[] supportedArgumentCounts,
			int passedArgumentCount,
			Location location);

		void AddActualTypeNotMatchingDeclaredTypeError(
			ValueUsageSummary definition,
			TypeDefinition actualType,
			TypeDefinition declaredType,
			Location location);

		void AddUnexpectedFieldOnCompositeDeclaredTypeError(
			ValueUsageSummary definition,
			Location location);

		void AddVariableUsageBeforeAssignmentError(
			ValueUsageSummary definition,
			Location location);

		void AddVariableDeclarationScopeConflictError(
			ValueUsageSummary definition,
			Location location);

		void AddFieldAndMethodNameConflictError(
			ValueUsageSummary definition,
			Location location);

		void AddNonConstantMethodArgumentError(
			string methodName,
			int argumentPosition,
			Location location);

		void AddUnexpectedMethodOnCompositeDeclaredTypeError(
			ValueUsageSummary definition,
			Location location);
	}
}