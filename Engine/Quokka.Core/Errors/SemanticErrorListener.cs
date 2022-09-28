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
	internal class SemanticErrorListener : ISemanticErrorListener
	{
		private readonly Dictionary<Type, SemanticErrorSubListenerBase> registeredSubListeners = 
			new Dictionary<Type, SemanticErrorSubListenerBase>();

		private readonly List<SemanticError> errors = new List<SemanticError>();

		public TSemanticErrorSubListener GetRegisteredSubListener<TSemanticErrorSubListener>()
			where TSemanticErrorSubListener : SemanticErrorSubListenerBase
		{
			var requestedSubListenerType = typeof(TSemanticErrorSubListener);

			if (!registeredSubListeners.ContainsKey(requestedSubListenerType))
				throw new InvalidOperationException($"{requestedSubListenerType} is not registered sublistener");

			var subListenerBase = registeredSubListeners[requestedSubListenerType];
			var subListener = subListenerBase as TSemanticErrorSubListener;
			if (subListener == null)
				throw new InvalidOperationException($"{subListenerBase.GetType()} is not {requestedSubListenerType}");

			return subListener;
		}

		public void RegisterSubListener(SemanticErrorSubListenerBase subListener)
		{
			var subListenerType = subListener.GetType();
			if (registeredSubListeners.ContainsKey(subListenerType))
				throw new InvalidOperationException($"SubListener of type {subListenerType} is already registered");

			registeredSubListeners[subListenerType] = subListener;
			subListener.Register(error => AddError(error));
		}

		protected void AddError(SemanticError error)
		{
			errors.Add(error);
		}

		public IReadOnlyCollection<ITemplateError> GetErrors()
		{
			return errors.AsReadOnly();
		} 

		public void AddInconsistentVariableTypingError(
			ValueUsageSummary definition,
			ValueUsage faultyOccurence,
			TypeDefinition correctType)
		{
			AddError(new SemanticError(
				$"Value of \"{definition.FullName}\" can't be used as a value of type {faultyOccurence.RequiredType}, " +
				$"due to usages as {correctType}",
				faultyOccurence.Location));
		}

		public void AddUndefinedFunctionError(string functionName, Location location)
		{
			AddError(new SemanticError(
				$"Unknown function \"{functionName}\"",
				location));
		}

		public void AddInvalidFunctionArgumentTypeError(string functionName,
			string argumentName,
			TypeDefinition realType,
			TypeDefinition expectedType,
			Location location)
		{
			AddError(new SemanticError(
				$"Type of an argument \"{argumentName}\" of a function \"{functionName}\" is incorrect: " +
				$"Expected {expectedType.Name}, but got {realType.Name}",
				location));
		}

		public void AddInvalidFunctionArgumentValueError(
			string functionName,
			string argumentName, 
			string message,
			Location location)
		{
			AddError(new SemanticError(
				$"Argument \"{argumentName}\" of a function \"{functionName}\" has incorrect value: {message}",
				location));
		}

		public void AddInvalidFunctionArgumentCountError(
			string functionName,
			int[] supportedArgumentCounts,
			int passedArgumentCount,
			Location location)
		{
			var supportedArgumentString = string.Join(", ", supportedArgumentCounts) ;
			
			AddError(new SemanticError(
				$"Function \"{functionName}\" should be called with ({supportedArgumentString}) arguments, but got {passedArgumentCount}",
				location));
		}

		public void AddInvalidFunctionResultTypeError(string functionName, 
			TypeDefinition expectedType, 
			TypeDefinition realType, 
			Location location)
		{
			AddError(new SemanticError(
				$"Function {functionName} has incorrect result type. Expected {expectedType.Name}, but got {realType.Name}",
				location));
		}

		public void AddActualTypeNotMatchingDeclaredTypeError(
			ValueUsageSummary definition,
			TypeDefinition actualType,
			TypeDefinition declaredType,
			Location location)
		{
			AddError(new SemanticError(
				$"Value of \"{definition.FullName}\" can't be used as a type {actualType}, " +
				$"due to usages as {declaredType}",
				location));
		}

		public void AddUnexpectedFieldOnCompositeDeclaredTypeError(
			ValueUsageSummary definition,
			Location location)
		{
			AddError(new SemanticError(
				$"Unknown parameter \"{definition.FullName}\"",
				location));
		}

		public void AddUnexpectedMethodOnCompositeDeclaredTypeError(
			ValueUsageSummary definition,
			Location location)
		{
			AddError(new SemanticError(
				$"Unknown function \"{definition.FullName}\"",
				location));
		}

		public void AddVariableDeclarationScopeConflictError(ValueUsageSummary definition, Location location)
		{
			AddError(new SemanticError(
				$"Variable name \"{definition.FullName}\" has the same name as an already declared variable",
				location));
		}

		public void AddFieldAndMethodNameConflictError(ValueUsageSummary definition, Location location)
		{
			AddError(new SemanticError(
				$"Field \"{definition.FullName}\" has a name conflict with a function in the same object",
				location));
		}

		public void AddNonConstantMethodArgumentError(string methodName, int argumentPosition, Location location)
		{
			AddError(new SemanticError(
				$"Method {methodName} got non constant value as an argument with position {argumentPosition}",
				location));
		}

		public void AddVariableUsageBeforeAssignmentError(ValueUsageSummary definition, Location location)
		{
			AddError(new SemanticError(
				$"Value of a variable \"{definition.FullName}\" is used before variable assignment",
				location));
		}
	}
}
