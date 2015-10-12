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

		public IReadOnlyCollection<SemanticError> GetErrors()
		{
			return errors.AsReadOnly();
		} 

		public void AddInconsistentVariableTypingError(
			VariableDefinition definition,
			VariableOccurence faultyOccurence,
			VariableType correctType)
		{
			AddError(new SemanticError(
				$"Параметр {definition.FullName} не может использоваться как {faultyOccurence.RequiredType}, " +
				$"так как в других местах он используется как {correctType}",
				faultyOccurence.Location));
		}

		public void AddUndefinedFunctionError(string functionName, Location location)
		{
			AddError(new SemanticError(
				$"Неизвестная функция {functionName}",
				location));
		}
	}
}
