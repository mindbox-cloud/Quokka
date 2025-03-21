﻿// // Copyright 2022 Mindbox Ltd
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
using System.Globalization;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class RenderStandardFunctionTests
	{
		[TestMethod]
		public void Render_Function_RandomText_DoesSomethingWithTwoArguments()
		{
			var template = new Template("${ chooseRandomText(\"Marilyn\", \"Keira\") }");

			var result = template.Render(new CompositeModelValue());

			Assert.IsTrue(result == "Marilyn" || result == "Keira");
		}

		[TestMethod]
		public void Render_Function_RandomText_DoesSomethingWithSingleArgument()
		{
			var template = new Template("${ chooseRandomText(\"Marilyn\") }");

			var result = template.Render(new CompositeModelValue());

			Assert.AreEqual("Marilyn", result);
		}

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

			Assert.AreEqual("2.54", result);
		}
		

		[TestMethod]
		public void Render_Function_FormatDecimal_EmptyFormat()
		{
			var template = new Template("${ formatDecimal(Value, \"\") }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Value", 2.53511m)));

			Assert.AreEqual("2.53511", result);
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
		public void Render_IfDecimalNull_Works()
		{
			var template = new Template("${ if( Price != NULL, formatDecimal(Price, \".00\"), \"Unknown price\") }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Price", new PrimitiveModelValue(45345.5m))));

			Assert.AreEqual("45345.50", result);
		}

		[TestMethod]
		public void Render_FunctionWithSingleQuotedValue_Works()
		{
			var template = new Template("${ toUpper('lowered') }");

			var result = template.Render(
				new CompositeModelValue());

			Assert.AreEqual("LOWERED", result);
		}

		[TestMethod]
		public void Render_AddDays_PositiveInt_Works()
		{
			var template = new Template("${ formatDateTime(AddDays(Date, 3), 'yyyy.MM.dd HH:mm:ss') }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Date", new DateTime(2016, 01, 01, 15, 03, 05))));

			Assert.AreEqual("2016.01.04 15:03:05", result);
		}

		[TestMethod]
		public void Render_AddDays_NegativeInt_Works()
		{
			var template = new Template("${ formatDateTime(AddDays(Date, -3), 'yyyy.MM.dd HH:mm:ss') }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Date", new DateTime(2016, 01, 15, 15, 03, 05))));

			Assert.AreEqual("2016.01.12 15:03:05", result);
		}



		[TestMethod]
		public void Render_AddDays_Zero_Works()
		{
			var template = new Template("${ formatDateTime(AddDays(Date, 0), 'yyyy.MM.dd HH:mm:ss') }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Date", new DateTime(2016, 01, 15, 15, 03, 05))));

			Assert.AreEqual("2016.01.15 15:03:05", result);
		}

		[TestMethod]
		public void Render_AddDays_PositiveDecimal_Works()
		{
			var template = new Template("${ formatDateTime(AddDays(Date, DateOffset), 'yyyy.MM.dd HH:mm:ss') }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Date", new DateTime(2016, 01, 01, 00, 00, 00)),
					new ModelField("DateOffset", 1.5m)));

			Assert.AreEqual("2016.01.02 12:00:00", result);
		}		
		
		[TestMethod]
		public void Render_AddHours_PositiveInt_Works()
		{
			var template = new Template("${ formatDateTime(AddHours(Date, 3), 'yyyy.MM.dd HH:mm:ss') }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Date", new DateTime(2016, 01, 01, 15, 03, 05))));

			Assert.AreEqual("2016.01.01 18:03:05", result);
		}	
		
		[TestMethod]
		public void Render_AddHours_NegativeInt_Works()
		{
			var template = new Template("${ formatDateTime(AddHours(Date, -3), 'yyyy.MM.dd HH:mm:ss') }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Date", new DateTime(2016, 01, 01, 15, 03, 05))));

			Assert.AreEqual("2016.01.01 12:03:05", result);
		}	
		
		[TestMethod]
		public void Render_AddHours_PositiveDecimal_Works()
		{
			var template = new Template("${ formatDateTime(AddHours(Date, 1.5), 'yyyy.MM.dd HH:mm:ss') }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Date", new DateTime(2016, 01, 01, 15, 03, 05))));

			Assert.AreEqual("2016.01.01 16:33:05", result);
		}	

		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void Render_Function_RandomText_WorksWithoutArguments()
		{
			var template = new Template("${ chooseRandomText() }");

			template.Render(new CompositeModelValue());
		}

		[TestMethod]
		public void Floor_NoDecimalPlaces()
		{
			var template = new Template(@"${ floor(value)}");

			var result = template.Render(
				new CompositeModelValue(new ModelField("value", 10m)));

			Assert.AreEqual("10", result);
		}

		[TestMethod]
		public void Floor_WithDecimalPlaces()
		{
			var template = new Template(@"${ floor(value)}");

			var result = template.Render(
				new CompositeModelValue(new ModelField("value", 10.99m)));

			Assert.AreEqual("10", result);
		}

		[TestMethod]
		public void Ceiling_NoDecimalPlaces()
		{
			var template = new Template(@"${ ceiling(value)}");

			var result = template.Render(
				new CompositeModelValue(new ModelField("value", 10m)));

			Assert.AreEqual("10", result);
		}

		[TestMethod]
		public void Ceiling_WithDecimalPlaces()
		{
			var template = new Template(@"${ ceiling(value)}");

			var result = template.Render(
				new CompositeModelValue(new ModelField("value", 10.99m)));

			Assert.AreEqual("11", result);
		}

		[TestMethod]
		public void SubstringWithIndex()
		{
			var template = new Template(@"${ substring(value, 2)}");

			var result = template.Render(
				new CompositeModelValue(new ModelField("value", "testValue")));

			Assert.AreEqual("estValue", result);
		}

		[TestMethod]
		public void SubstringWithIndexAndLength()
		{
			var template = new Template(@"${ substring(value, 2, 3)}");

			var result = template.Render(
				new CompositeModelValue(new ModelField("value", "testValue")));

			Assert.AreEqual("est", result);
		}

		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void SubstringWithWrongArgumentsCount()
		{
			var template = new Template(@"${ substring(value, 2, 3, 4)}");

			var result = template.Render(
				new CompositeModelValue(new ModelField("value", "testValue")));
		}

		[TestMethod]
		public void Length()
		{
			var template = new Template(@"${ length('abcd ') }");

			var result = template.Render(new CompositeModelValue());

			Assert.AreEqual("5", result);
		}

		[TestMethod]
		public void GetDay()
		{
			var template = new Template(@"${ GetDay(value)}");

			var result = template.Render(
				new CompositeModelValue(new ModelField("value", new DateTime(2018, 11, 15))));

			Assert.AreEqual("15", result);
		}

		[TestMethod]
		public void GetMonth()
		{
			var template = new Template(@"${ GetMonth(value)}");

			var result = template.Render(
				new CompositeModelValue(new ModelField("value", new DateTime(2018, 11, 15))));

			Assert.AreEqual("11", result);
		}

		[TestMethod]
		public void GetYear()
		{
			var template = new Template(@"${ GetYear(value)}");

			var result = template.Render(
				new CompositeModelValue(new ModelField("value", new DateTime(2018, 11, 15))));

			Assert.AreEqual("2018", result);
		}

		[TestMethod]
		public void GetDay_GetMonth_GetYear_Arithmetic()
		{
			var template = new Template(@"${GetDay(value) + GetMonth(value) + GetYear(value)}");

			var result = template.Render(
				new CompositeModelValue(new ModelField("value", new DateTime(2018, 11, 15))));

			Assert.AreEqual("2044", result);
		}
		
		[TestMethod]
		public void ToUnixTimeStamp_When_Utc()
		{
			var template = new Template("${ ToUnixTimeStamp(value) }");

			var dt = DateTime.SpecifyKind(new DateTime(2020, 01, 01, 00, 00, 00), DateTimeKind.Utc);
			var result = template.Render(
				new CompositeModelValue(new ModelField("value", dt)));

			Assert.AreEqual("1577836800", result);
		}
				
		[TestMethod]
		public void ToUnixTimeStamp_When_Unspecified()
		{
			var template = new Template("${ ToUnixTimeStamp(value) }");

			var dt = new DateTime(2020, 01, 01, 00, 00, 00, DateTimeKind.Unspecified);
			var result = template.Render(
				new CompositeModelValue(new ModelField("value", dt)));

			var utcDate = DateTime.SpecifyKind(new DateTime(2020, 01, 01, 00, 00, 00), DateTimeKind.Utc);
			var delta = (utcDate - dt.ToUniversalTime()).TotalSeconds;
			
			Assert.AreEqual($"{1577836800 - delta}", result);
		}
	}
}
