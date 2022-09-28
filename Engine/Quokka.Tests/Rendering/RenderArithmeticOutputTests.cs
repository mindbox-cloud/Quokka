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

using System.Globalization;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class RenderArithmeticOutputTests
	{
		[TestMethod]
		public void Render_ArithmeticOutput_IntegerConstant()
		{
			var template = new Template("${ 335 }");

			var result = template.Render(
				new CompositeModelValue());
			Assert.AreEqual("335", result);
		}
		
		[TestMethod]
		public void Render_ArithmeticOutput_DecimalConstant()
		{
			var template = new Template("${ 24.05 }");

			var result = template.Render(
				new CompositeModelValue());
			Assert.AreEqual("24.05", result);
		}

		[TestMethod]
		public void Render_ArithmeticOutput_SimplePlus()
		{
			var template = new Template("${ 4 + 7 }");

			var result = template.Render(
				new CompositeModelValue());
			Assert.AreEqual("11", result);
		}

		[TestMethod]
		public void Render_ArithmeticOutput_SimpleMinus()
		{
			var template = new Template("${ 65 - 24 }");

			var result = template.Render(
				new CompositeModelValue());

			Assert.AreEqual("41", result);
		}

		[TestMethod]
		public void Render_ArithmeticOutput_ComplexPlusMinus()
		{
			var template = new Template("${ 4 + 7 - 13 + 15 }");

			var result = template.Render(
				new CompositeModelValue());

			Assert.AreEqual("13", result);
		}

		[TestMethod]
		public void Render_ArithmeticOutput_SimpleMultiplication()
		{
			var template = new Template("${ 6*9 }");

			var result = template.Render(
				new CompositeModelValue());

			Assert.AreEqual("54", result);
		}

		[TestMethod]
		public void Render_ArithmeticOutput_MultiplicationWithDecimal()
		{
			var template = new Template("${ 6*0.1 }");

			var result = template.Render(
				new CompositeModelValue());

			Assert.AreEqual("0.6", result);
		}


		[TestMethod]
		public void Render_ArithmeticOutput_SimpleDivision()
		{
			var template = new Template("${ 85 / 5 }");

			var result = template.Render(
				new CompositeModelValue());

			Assert.AreEqual("17", result);
		}

		[TestMethod]
		public void Render_ArithmeticOutput_ComplexMultiplyDivide()
		{
			var template = new Template("${ 5 * 8 / 2 * 3 }");

			var result = template.Render(
				new CompositeModelValue());

			Assert.AreEqual("60", result);
		}

		[TestMethod]
		public void Render_ArithmeticOutput_DivisionWithDecimalPoints()
		{
			var template = new Template("${ 1/3 }");

			var result = template.Render(
				new CompositeModelValue());

			Assert.AreEqual("0.33", result);
		}

		[TestMethod]
		public void Render_ArithmeticOutput_SimpleNegation()
		{
			var template = new Template("${ -(4 + 7) }");

			var result = template.Render(
				new CompositeModelValue());

			Assert.AreEqual("-11", result);
		}

		[TestMethod]
		public void Render_ArithmeticOutput_ComplexExpression()
		{
			// A smoke test
			var template = new Template("${ (24 + 3) + (5 * 25)/7 - (6 * 7 / 8*9) * (242) + A.Value * B.Value.Length }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A",
						new CompositeModelValue(
							new ModelField("Value", 41))),
					new ModelField("B",
						new CompositeModelValue(
							new ModelField("Value",
								new CompositeModelValue(
									new ModelField("Length", 77)))))));

			Assert.AreEqual("-8232.64", result);
		}

		[TestMethod]
		public void Render_ArithmeticOutput_ArithmeticMethodResults()
		{
			// A smoke test
			var template = new Template("${ Math.Min(32, 16) + Math.Square(6) + Math.DecimalPart() }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Math",
						new CompositeModelValue(
							new ModelMethod("Min", new object[] { 32, 16 }, 16),
							new ModelMethod("Square", new object[] { 6 }, 36),
							new ModelMethod("DecimalPart", 0.45m)
							))
					));

			Assert.AreEqual("52.45", result);
		}
	}
}
