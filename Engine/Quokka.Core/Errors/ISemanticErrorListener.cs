using System;

namespace Mindbox.Quokka
{
	internal interface ISemanticErrorListener : IErrorListener
	{
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
			int requiredArgumentCount,
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
	}
}