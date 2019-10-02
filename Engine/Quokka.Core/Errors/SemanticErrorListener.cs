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
				$"Значение \"{definition.FullName}\" не может использоваться как {faultyOccurence.RequiredType}, " +
				$"так как в других местах оно используется как {correctType}",
				faultyOccurence.Location));
		}

		public void AddUndefinedFunctionError(string functionName, Location location)
		{
			AddError(new SemanticError(
				$"Неизвестная функция \"{functionName}\"",
				location));
		}

		public void AddInvalidFunctionArgumentTypeError(string functionName,
			string argumentName,
			TypeDefinition realType,
			TypeDefinition expectedType,
			Location location)
		{
			AddError(new SemanticError(
				$"Недопустимый тип аргумента \"{argumentName}\" функции \"{functionName}\": " +
				$"Ожидался {expectedType.Name}, а передан {realType.Name}",
				location));
		}

		public void AddInvalidFunctionArgumentValueError(
			string functionName,
			string argumentName, 
			string message,
			Location location)
		{
			AddError(new SemanticError(
				$"Недопустимое значение аргумента \"{argumentName}\" функции \"{functionName}\": {message}",
				location));
		}

		public void AddInvalidFunctionArgumentCountError(
			string functionName,
			int[] supportedArgumentCounts,
			int passedArgumentCount,
			Location location)
		{
			AddError(new SemanticError(
				$"Функции \"{functionName}\" вместо ожидаемого количества аргументов ({string.Join(", ", supportedArgumentCounts)}) передано {passedArgumentCount}",
				location));
		}

		public void AddInvalidFunctionResultTypeError(string functionName, 
			TypeDefinition expectedType, 
			TypeDefinition realType, 
			Location location)
		{
			AddError(new SemanticError(
				$"Недопустимый тип результата функции {functionName}. Ожидался {expectedType.Name}, а она возвращает {realType.Name}",
				location));
		}

		public void AddActualTypeNotMatchingDeclaredTypeError(
			ValueUsageSummary definition,
			TypeDefinition actualType,
			TypeDefinition declaredType,
			Location location)
		{
			AddError(new SemanticError(
				$"Параметр \"{definition.FullName}\" не может использоваться как {actualType}, " +
				$"так как в других местах он используется как {declaredType}",
				location));
		}

		public void AddUnexpectedFieldOnCompositeDeclaredTypeError(
			ValueUsageSummary definition,
			Location location)
		{
			AddError(new SemanticError(
				$"Неизвестный параметр \"{definition.FullName}\"",
				location));
		}

		public void AddUnexpectedMethodOnCompositeDeclaredTypeError(
			ValueUsageSummary definition,
			Location location)
		{
			AddError(new SemanticError(
				$"Неизвестный метод \"{definition.FullName}\"",
				location));
		}

		public void AddVariableDeclarationScopeConflictError(ValueUsageSummary definition, Location location)
		{
			AddError(new SemanticError(
				$"Имя переменной \"{definition.FullName}\" конфликтует с другой переменной, объявленной выше или ниже",
				location));
		}

		public void AddFieldAndMethodNameConflictError(ValueUsageSummary definition, Location location)
		{
			AddError(new SemanticError(
				$"Имя поля \"{definition.FullName}\" совпадает с именем метода этого же объекта",
				location));
		}

		public void AddNonConstantMethodArgumentError(string methodName, int argumentPosition, Location location)
		{
			AddError(new SemanticError(
					$"В метод {methodName} в качестве аргумента под номером {argumentPosition} передано не константное значение",
					location));
		}

		public void AddVariableUsageBeforeAssignmentError(ValueUsageSummary definition, Location location)
		{
			AddError(new SemanticError(
				$"Переменная \"{definition.FullName}\" используется до присваивания значения.",
				location));
		}
	}
}
