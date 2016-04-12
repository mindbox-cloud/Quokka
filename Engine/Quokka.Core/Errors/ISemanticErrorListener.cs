using System;

namespace Quokka
{
	internal interface ISemanticErrorListener : IErrorListener
	{
		void AddInconsistentVariableTypingError(
			VariableDefinition definition,
			VariableOccurence faultyOccurence,
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
			int requiredArgumentCount,
			int passedArgumentCount,
			Location location);

		void AddActualTypeNotMatchingDeclaredTypeError(
			VariableDefinition definition,
			TypeDefinition actualType,
			TypeDefinition declaredType,
			Location location);

		void AddUnexpectedFieldOnCompositeDeclaredTypeError(
			VariableDefinition definition,
			Location location);

		void AddVariableDeclarationScopeConflictError(
			VariableDefinition definition,
			Location location);
	}
}