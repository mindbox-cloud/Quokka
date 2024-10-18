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

namespace Mindbox.Quokka
{
	internal abstract class AdditionOperand : IVisitable
	{
		public ArithmeticExpression Expression { get; }

		protected AdditionOperand(ArithmeticExpression expression)
		{
			Expression = expression;
		}

		public abstract double Calculate(double leftOperand, RenderContext renderContext);
		
		public abstract void Accept(ITreeVisitor treeVisitor);

		public static AdditionOperand Plus(ArithmeticExpression expression)
		{
			return new PlusOperand(expression);
		}

		public static AdditionOperand Minus(ArithmeticExpression expression)
		{
			return new MinusOperand(expression);
		}
		
		private class PlusOperand : AdditionOperand
		{
			public PlusOperand(ArithmeticExpression expression)
				: base(expression)
			{
			}

			public override double Calculate(double leftOperand, RenderContext renderContext)
			{
				return leftOperand + Expression.GetValue(renderContext);
			}

			public override void Accept(ITreeVisitor treeVisitor)
			{
				treeVisitor.VisitPlusOperand();

				Expression.Accept(treeVisitor);
			
				treeVisitor.EndVisit();
			}
		}

		private class MinusOperand : AdditionOperand
		{
			public MinusOperand(ArithmeticExpression expression)
				: base(expression)
			{
			}

			public override double Calculate(double leftOperand, RenderContext renderContext)
			{
				return leftOperand - Expression.GetValue(renderContext);
			}

			public override void Accept(ITreeVisitor treeVisitor)
			{
				treeVisitor.VisitMinusOperand();

				Expression.Accept(treeVisitor);
			
				treeVisitor.EndVisit();
			}
		}
	}
}
