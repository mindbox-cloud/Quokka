using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
	internal class VariableValueExpression : VariantValueExpression
	{
		private readonly string variableName;

		private readonly Location variableLocation;

		public VariableValueExpression(string variableName, Location variableLocation)
		{
			this.variableName = variableName;
			this.variableLocation = variableLocation;
		}

		public string StringRepresentation => variableName;
		
		public override void PerformSemanticAnalysis(AnalysisContext context, TypeDefinition expectedExpressionType)
		{
			context.VariableScope.RegisterVariableValueUsage(
				variableName,
				new ValueUsage(
					variableLocation,
					expectedExpressionType));
		}

		public ValueUsageSummary GetVariableDefinition(AnalysisContext context)
		{
			return context.VariableScope.TryGetVariableDefinition(variableName);
		}

		public override void RegisterIterationOverExpressionResult(AnalysisContext context, ValueUsageSummary iterationVariable)
		{
			var existingVariableDefinition = context.VariableScope.TryGetVariableDefinition(variableName);
			if (existingVariableDefinition == null)
				throw new InvalidOperationException($"Variable definition for variable {variableName} not found");

			existingVariableDefinition.AddEnumerationResultUsageSummary(iterationVariable);
		}

		public override IModelDefinition GetExpressionResultModelDefinition(AnalysisContext context)
		{
			return new PrimitiveModelDefinition(TypeDefinition.Unknown);
		}
		
		public override VariableValueStorage Evaluate(RenderContext renderContext)
		{
			var valueStorage = TryGetValueStorage(renderContext);
			if (valueStorage == null || valueStorage.CheckIfValueIsNull())
				throw new UnrenderableTemplateModelException(
					$"An attempt to use the value of variable \"{variableName}\" which happens to be null",
					variableLocation);

			return valueStorage;
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			var valueStorage = TryGetValueStorage(renderContext);
			return valueStorage == null || valueStorage.CheckIfValueIsNull();
		}
		
		public VariableValueStorage TryGetValueStorage(RenderContext renderContext)
		{
			return renderContext.VariableScope.TryGetValueStorageForVariable(variableName);
		}
	}
}
