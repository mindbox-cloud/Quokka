using System;
using System.Collections.Generic;

namespace Quokka
{
	internal class SemanticErrorListener : ISemanticErrorListener
	{
		private readonly List<SemanticError> errors = new List<SemanticError>();

		protected void AddError(SemanticError error)
		{
			errors.Add(error);
		}

		public IReadOnlyCollection<ITemplateError> GetErrors()
		{
			return errors.AsReadOnly();
		} 

		public void AddInconsistentVariableTypingError(
			VariableDefinition definition,
			VariableOccurence faultyOccurence,
			TypeDefinition correctType)
		{
			AddError(new SemanticError(
				$"Параметр \"{definition.FullName}\" не может использоваться как {faultyOccurence.RequiredType}, " +
				$"так как в других местах он используется как {correctType}",
				faultyOccurence.Location));
		}

		public void AddUndefinedFunctionError(string functionName, Location location)
		{
			AddError(new SemanticError(
				$"Неизвестная функция \"{functionName}\"",
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

		public void AddInvalidFunctionResultTypeError(string functionName, 
			TypeDefinition expectedType, 
			TypeDefinition realType, 
			Location location)
		{
			AddError(new SemanticError(
				$"Недопустимый тип результата функции {functionName}. Ожидался {expectedType.Name}, а она возвращает {realType.Name}",
				location));
		}
	}
}
