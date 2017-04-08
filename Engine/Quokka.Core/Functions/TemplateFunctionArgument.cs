using System;
using System.Linq;

namespace Mindbox.Quokka
{
	public abstract class TemplateFunctionArgument
	{
		public string Name { get; }
		internal abstract TypeDefinition Type { get; }

		protected TemplateFunctionArgument(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Argument name should not be null or blank", nameof(name));

			Name = name;
		}

		internal abstract ArgumentValueValidationResult ValidateValue(VariableValueStorage value);

		internal virtual void MapArgumentValueToResult(
			SemanticAnalysisContext context,
			VariableDefinition resultDefinition,
			VariableDefinition argumentVariableDefinition)
		{
		}
	}
}