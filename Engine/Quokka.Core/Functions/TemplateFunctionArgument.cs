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

		/// <summary>
		/// Performs additional semantic analysis on expressions used argument values based on usages
		/// of function result.
		/// Some function return a "projection" of its argument (array or other value) meaning that the subsequent usages of
		/// function result should be considered when determining the exact type of said array or other value.
		/// </summary>
		/// <remarks>
		/// This mechanism isn't available for third-party functions. Functions that use this mechanism
		/// are designed with strong understanding of implementation details of the templating process.
		/// </remarks>
		internal virtual void AnalyzeArgumentValueBasedOnFunctionResultUsages(
			SemanticAnalysisContext context,
			VariableDefinition resultVariableDefinition,
			IExpression argumentValueExpression)
		{
		}
	}
}