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

using System;
using System.IO;
using System.Text;

namespace Mindbox.Quokka
{
	internal class OutputInstructionBlock : TemplateNodeBase, IStaticBlockPart
	{
		public readonly IExpression expression;

		public int Offset { get; }
		public int Length { get; }

		public OutputInstructionBlock(IExpression expression, int offset, int length)
		{
			this.expression = expression;
			Offset = offset;
			Length = length;
			// Console.WriteLine(ObjectDumper.Dump(expression, DumpStyle.CSharp));

		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			expression.PerformSemanticAnalysis(context, TypeDefinition.Primitive);
		}

		public override void Render(TextWriter resultWriter, RenderContext renderContext)
		{
			resultWriter.Write(expression.GetOutputValue(renderContext));
		}

		public override BlockDTO GetTreeDTO()
		{
			var dto = base.GetTreeDTO();
			dto.expression = expression.GetTreeDTO();
			dto.type = "OutputInstructionBlock";

			return dto;
		}
	}
}
