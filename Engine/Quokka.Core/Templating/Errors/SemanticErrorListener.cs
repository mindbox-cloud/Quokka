using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class SemanticErrorListener : ISemanticErrorListener
	{
		private readonly List<Error> errors = new List<Error>();

		protected void AddError(Error error)
		{
			errors.Add(error);
		}

		public IReadOnlyCollection<Error> GetErrors()
		{
			return errors.AsReadOnly();
		} 

		public void AddInconsistentVariableTypesError(VariableDefinition definition, VariableOccurence occurence)
		{
			AddError(new Error($"Параметр {definition.FullName} не может использоваться как {occurence.RequiredType}, " +
								$"так как он уже используется в качестве {definition.Type}"));
		}

	}
}
