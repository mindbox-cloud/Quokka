using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka.Errors
{
	internal class ModelDefinitionErrorListener
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

		public void AddInconsistenDefinitionTypesError(string fieldName)
		{
			AddError(new SemanticError(
				$"Field {fieldName} inferred to different types", null));
		}
	}
}
