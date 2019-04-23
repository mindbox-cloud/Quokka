using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
	internal class AssignmentBlock : TemplateNodeBase
	{
		private readonly string variableName;
		private readonly IExpression value;
		private readonly Location location;

		public AssignmentBlock(string variableName, IExpression value, Location location)
		{
			this.variableName = variableName;
			this.value = value;
			this.location = location;
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			value.PerformSemanticAnalysis(context, TypeDefinition.Primitive);

			var destinationVariable = context.VariableScope.RegisterScopedVariableValueUsage(
				variableName, new ValueUsage(location, GetEffectiveValueUsageType(context), VariableUsageIntention.Write));

			value.RegisterAssignmentToVariable(context, destinationVariable);
		}

		// We want situations where a variable is initialized with an integer value and than assigned with a decimal value
		// to be valid and compile successfully. This hack allows such behavior, but it should be rewritten in more comprehensive
		// and generic way in future.
		private TypeDefinition GetEffectiveValueUsageType(AnalysisContext context)
		{
			var valueUsageType = value.GetResultType(context);
			if (valueUsageType != TypeDefinition.Integer)
				return valueUsageType;

			return TypeDefinition.Decimal;
		}

		public override void Render(StringBuilder resultBuilder, RenderContext renderContext)
		{
			var valueStorage = value.Evaluate(renderContext);

			renderContext.VariableScope.TrySetValueStorageForVariable(variableName, valueStorage);
		}
	}
}
