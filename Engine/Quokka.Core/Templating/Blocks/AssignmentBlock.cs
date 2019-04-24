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
				variableName, new ValueUsage(location, value.GetResultType(context), VariableUsageIntention.Write));

			value.RegisterAssignmentToVariable(context, destinationVariable);
		}

		public override void Render(StringBuilder resultBuilder, RenderContext renderContext)
		{
			var valueStorage = value.Evaluate(renderContext);

			renderContext.VariableScope.TrySetValueStorageForVariable(variableName, valueStorage);
		}
	}
}
