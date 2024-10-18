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
	internal abstract class MultiplicationOperand : IVisitable
	{
		public ArithmeticExpression Expression { get; }

		protected MultiplicationOperand(ArithmeticExpression expression)
		{
			Expression = expression;
		}

		public abstract double Calculate(double leftOperand, RenderContext renderContext);

		public static MultiplicationOperand Multiply(ArithmeticExpression expression)
		{
			return new MultOperand(expression);
		}

		public static MultiplicationOperand Divide(ArithmeticExpression expression)
		{
			return new DivOperand(expression);
		}
		
		public abstract void Accept(ITreeVisitor treeVisitor);
		
		private class MultOperand : MultiplicationOperand
		{
			public MultOperand(ArithmeticExpression expression)
				: base(expression)
			{
			}

			public override double Calculate(double leftOperand, RenderContext renderContext)
			{
				return leftOperand * Expression.GetValue(renderContext);
			}
			
			public override void Accept(ITreeVisitor treeVisitor)
			{
				treeVisitor.VisitMultOperand();
				
				Expression.Accept(treeVisitor);
				
				treeVisitor.EndVisit();
			}
		}

		private class DivOperand : MultiplicationOperand
		{
			public DivOperand(ArithmeticExpression expression)
				: base(expression)
			{
			}

			public override double Calculate(double leftOperand, RenderContext renderContext)
			{
				return leftOperand / Expression.GetValue(renderContext);
			}

			public override void Accept(ITreeVisitor treeVisitor)
			{
				treeVisitor.VisitDivOperand();
				
				Expression.Accept(treeVisitor);
				
				treeVisitor.EndVisit();
			}
		}
	}
}
