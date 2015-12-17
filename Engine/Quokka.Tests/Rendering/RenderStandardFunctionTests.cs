using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class RenderStandardFunctionTests
	{
		[TestMethod]
		public void Render_Function_ReplaceIfEmpty_FirstOptionNotEmpty()
		{
			var template = new Template("${ replaceIfEmpty(\"Marilyn\", \"Keira\") }");

			var result = template.Render(new CompositeModelValue());

			Assert.AreEqual("Marilyn", result);
		}

		[TestMethod]
		public void Render_Function_ReplaceIfEmpty_FirstOptionEmpty()
		{
			var template = new Template("${ replaceIfEmpty(\"\", \"Keira\") }");

			var result = template.Render(new CompositeModelValue());

			Assert.AreEqual("Keira", result);
		}
		
		[TestMethod]
		public void Render_Function_ReplaceIfEmpty_FirstOptionWhiteSpace()
		{
			var template = new Template("${ replaceIfEmpty(\"  \", \"Keira\") }");

			var result = template.Render(new CompositeModelValue());

			Assert.AreEqual("Keira", result);
		}

		[TestMethod]
		public void Render_Function_ReplaceIfEmpty_FirstOptionNonEmptyParameter()
		{
			var template = new Template("${ replaceIfEmpty(Value1, Value2) }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Value1", new PrimitiveModelValue("Plan A")),
					new ModelField("Value2", new PrimitiveModelValue("Plan B"))));

			Assert.AreEqual("Plan A", result);
		}

		[TestMethod]
		public void Render_Function_ReplaceIfEmpty_FirstOptionEmptyParameter()
		{
			var template = new Template("${ replaceIfEmpty(Value1, Value2) }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Value1", new PrimitiveModelValue("")),
					new ModelField("Value2", new PrimitiveModelValue("Plan B"))));

			Assert.AreEqual("Plan B", result);
		}

		[TestMethod]
		public void Render_Function_FormatDecimal_CorrectFormat()
		{
			var template = new Template("${ formatDecimal(Value, \"N2\") }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Value", 2.53511m)));

			Assert.AreEqual("2,54", result);
		}
		

		[TestMethod]
		public void Render_Function_FormatDecimal_EmptyFormat()
		{
			var template = new Template("${ formatDecimal(Value, \"\") }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Value", 2.53511m)));

			Assert.AreEqual("2,53511", result);
		}

		[TestMethod]
		public void Render_Function_FormatDecimal_Intvalue()
		{
			var template = new Template("${ formatDecimal(Value, \"\") }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Value", 2)));

			Assert.AreEqual("2", result);
		}

		[TestMethod]
		public void Render_Function_FormatDecimal_ArithmeticExpressionWithDecimal_WithFormat()
		{
			var template = new Template("${ formatDecimal(10000 - Value, \"0\") }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Value", 5555.55m)));

			Assert.AreEqual("4444", result);
		}

		[TestMethod]
		public void Render_Function_FormatDateTime_CorrectFormat()
		{
			var template = new Template("${ formatDateTime(Value, \"dd'.'MM'.'yy HH':'mm':'ss\") }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Value", new DateTime(2015, 10, 12, 22, 55, 55))));

			Assert.AreEqual("12.10.15 22:55:55", result);
		}

		[TestMethod]
		public void Render_Function_FormatTime_CorrectFormat()
		{
			var template = new Template("${ formatTime(Value, \"hh\\-mm\\-ss\") }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Value", TimeSpan.FromMinutes(5))));

			Assert.AreEqual("00-05-00", result);
		}

		[TestMethod]
		public void Render_Function_If_ConditionIsTrue()
		{
			var template = new Template("${ if(IsTest, \"test.example.com\", \"example.com\") }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("IsTest", true)));

			Assert.AreEqual("test.example.com", result);
		}

		[TestMethod]
		public void Render_Function_If_ConditionIsFalse()
		{
			var template = new Template("${ if(IsTest, \"test.example.com\", \"example.com\") }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("IsTest", false)));

			Assert.AreEqual("example.com", result);
		}

		[TestMethod]
		public void Render_Function_CaseInsensitivity()
		{
			var template = new Template("${ RePlAcEIFEMPTY(\"Marilyn\", \"Keira\") }");

			var result = template.Render(new CompositeModelValue());

			Assert.AreEqual("Marilyn", result);
		}

		[TestMethod]
		public void Render_Function_NestedIfs()
		{
			var template = new Template(@"
				${ if (IsTest, 
					""test.example.com"", 
					if (IsStaging, 
						""staging.example.com"", 
						""example.com"")) }
				");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("IsTest", false),
					new ModelField("IsStaging", false)));

			var expected = @"
				example.com
				";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IsEmpty_NotEmptyString()
		{
			var template = new Template("${ isEmpty(Value) }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Value", "matrix")));

			Assert.AreEqual(false.ToString(), result);
		}

		[TestMethod]
		public void Render_IsEmpty_EmptyString()
		{
			var template = new Template("${ isEmpty(Value) }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Value", "")));

			Assert.AreEqual(true.ToString(), result);
		}

		[TestMethod]
		public void Render_IsEmpty_Whitespace()
		{
			var template = new Template("${ isEmpty(Value) }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Value", " ")));

			Assert.AreEqual(true.ToString(), result);
		}

		[TestMethod]
		public void Render_IsEmptyAndIfCombination_Works()
		{
			var template = new Template("${ if( isEmpty(productionFlag), \"test.example.com\", \"example.com\") }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("productionFlag", "")));

			Assert.AreEqual("test.example.com", result);
		}
	}
}
