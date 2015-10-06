using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class ApplyArithmeticOutputTests
	{
		[TestMethod]
		public void Apply_ArithmeticOutput_SimplePlus()
		{
			var template = new Template("${ 4 + 7 }");

			var result = template.Apply(
				new CompositeParameterValue());

			Assert.AreEqual("11", result);
		}

		[TestMethod]
		public void Apply_ArithmeticOutput_SimpleMinus()
		{
			var template = new Template("${ 65 - 24 }");

			var result = template.Apply(
				new CompositeParameterValue());

			Assert.AreEqual("41", result);
		}

		[TestMethod]
		public void Apply_ArithmeticOutput_ComplexPlusMinus()
		{
			var template = new Template("${ 4 + 7 - 13 + 15 }");

			var result = template.Apply(
				new CompositeParameterValue());

			Assert.AreEqual("13", result);
		}

		[TestMethod]
		public void Apply_ArithmeticOutput_SimpleMultiplication()
		{
			var template = new Template("${ 6*9 }");

			var result = template.Apply(
				new CompositeParameterValue());

			Assert.AreEqual("54", result);
		}

		[TestMethod]
		public void Apply_ArithmeticOutput_SimpleDivision()
		{
			var template = new Template("${ 85 / 5 }");

			var result = template.Apply(
				new CompositeParameterValue());

			Assert.AreEqual("17", result);
		}

		[TestMethod]
		public void Apply_ArithmeticOutput_ComplexMultiplyDivide()
		{
			var template = new Template("${ 5 * 8 / 2 * 3 }");

			var result = template.Apply(
				new CompositeParameterValue());

			Assert.AreEqual("60", result);
		}

		[TestMethod]
		public void Apply_ArithmeticOutput_DivisionWithDecimalPoints()
		{
			var template = new Template("${ 1/3 }");

			var result = template.Apply(
				new CompositeParameterValue());

			Assert.AreEqual("0,33", result);
		}

		[TestMethod]
		public void Apply_ArithmeticOutput_SimpleNegation()
		{
			var template = new Template("${ -(4 + 7) }");

			var result = template.Apply(
				new CompositeParameterValue());

			Assert.AreEqual("-11", result);
		}

		[TestMethod]
		public void Apply_ArithmeticOutput_ComplexExpression()
		{
			// A smoke test
			var template = new Template("${ (24 + 3) + (5 * 25)/7 - (6 * 7 / 8*9) * (242) + A.Value * B.Value.Length }");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A",
						new CompositeParameterValue(
							new ParameterField("Value", 41))),
					new ParameterField("B",
						new CompositeParameterValue(
							new ParameterField("Value",
								new CompositeParameterValue(
									new ParameterField("Length", 77)))))));

			Assert.AreEqual("-8232,64", result);
		}
	}
}
