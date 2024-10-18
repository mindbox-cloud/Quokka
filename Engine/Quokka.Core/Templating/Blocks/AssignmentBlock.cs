// // Copyright 2022 Mindbox Ltd
// //
// // Licensed under the Apache License, Version 2.0 (the "License");
// // you may not use this file except in compliance with the License.
// // You may obtain a copy of the License at
// //
// //     http://www.apache.org/licenses/LICENSE-2.0
// //
// // Unless required by applicable law or agreed to in writing, software
// // distributed under the License is distributed on an "AS IS" BASIS,
// // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// // See the License for the specific language governing permissions and
// // limitations under the License.

using System.IO;

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

		public override void Render(TextWriter resultWriter, RenderContext renderContext)
		{
			var valueStorage = value.Evaluate(renderContext);

			renderContext.VariableScope.TrySetValueStorageForVariable(variableName, valueStorage);
		}

		public override void Accept(ITreeVisitor treeVisitor)
		{
			treeVisitor.VisitAssignmentBlock(variableName);
			
			value.Accept(treeVisitor);

			treeVisitor.EndVisit();
		}
	}
}
