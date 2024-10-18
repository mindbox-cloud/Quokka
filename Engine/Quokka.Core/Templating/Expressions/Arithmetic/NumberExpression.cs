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

namespace Mindbox.Quokka
{
	internal class NumberExpression : ArithmeticExpression
	{
		private readonly double number;

		public NumberExpression(double number)
		{
			this.number = number;
		}
		
		public override TypeDefinition GetResultType(AnalysisContext context)
		{
			return Math.Abs(number % 1) < Double.Epsilon
						? TypeDefinition.Integer
						: TypeDefinition.Decimal;
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			return false;
		}

		public override void Accept(ITreeVisitor treeVisitor)
		{
			treeVisitor.VisitNumberExpression(number);
			treeVisitor.EndVisit();
		}

		public override double GetValue(RenderContext renderContext)
		{
			return number;
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			// This node is constant and therefore can't affect semantic analysis context.
		}

		protected override bool TryGetStaticValue(out double value)
		{
			value = number;
			return true;
		}
	}
}