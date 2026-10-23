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
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests;

[TestClass]
public class RenderContainsFunctionTests
{
	private const string ContainsTemplate = "${ contains(Value, Search) }";

	[TestMethod]
	public void U1_Contains_IgnoresCase()
	{
		Assert.AreEqual(true.ToString(), Render("English", "english"));
	}

	[TestMethod]
	public void U2_Contains_FindsSubstring()
	{
		Assert.AreEqual(true.ToString(), Render("english for adults", "english"));
	}

	[TestMethod]
	public void U3_Contains_SubstringMissing_ReturnsFalse()
	{
		Assert.AreEqual(false.ToString(), Render("german for kids", "english"));
	}

	[TestMethod]
	public void U4_Contains_NullValue_ReturnsFalse()
	{
		var template = new Template(ContainsTemplate);

		var result = template.Render(
			new CompositeModelValue(
				new ModelField("Value", new PrimitiveModelValue(null)),
				new ModelField("Search", "english")));

		Assert.AreEqual(false.ToString(), result);
	}

	[TestMethod]
	[DataRow("")]
	[DataRow("   ")]
	public void U5_Contains_EmptyOrWhitespaceValue_ReturnsFalse(string value)
	{
		Assert.AreEqual(false.ToString(), Render(value, " "));
	}

	[TestMethod]
	[DataRow("")]
	[DataRow("   ")]
	public void U6_Contains_EmptyOrWhitespaceSearch_ReturnsFalse(string search)
	{
		Assert.AreEqual(false.ToString(), Render("english for adults", search));
	}

	[TestMethod]
	public void U6_Contains_NullSearch_ReturnsFalse()
	{
		var template = new Template(ContainsTemplate);

		var result = template.Render(
			new CompositeModelValue(
				new ModelField("Value", "english for adults"),
				new ModelField("Search", new PrimitiveModelValue(null))));

		Assert.AreEqual(false.ToString(), result);
	}

	[TestMethod]
	[DataRow("математика мини-курс", "block|tail")]
	[DataRow("английский", "tail")]
	public void U7_Contains_InsideIfBlock_SelectsBlock(string tariff, string expected)
	{
		var template = new Template("@{ if contains(Tariff, \"Математика\") }block|@{ end if }tail");

		var result = template.Render(
			new CompositeModelValue(
				new ModelField("Tariff", tariff)));

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	[DataRow("${ contains(Value) }")]
	[DataRow("${ contains(Value, Search, Other) }")]
	public void U8_Contains_WrongArgumentCount_FailsLikeOtherFunctions(string text)
	{
		_ = new Template(ContainsTemplate);
		Assert.ThrowsException<TemplateContainsErrorsException>(() => new Template("${ substring(Value) }"));
		Assert.ThrowsException<TemplateContainsErrorsException>(() => new Template(text));
	}

	[TestMethod]
	public void U9_Contains_NumberValue_RejectedLikeToUpper()
	{
		var number = new CompositeModelValue(new ModelField("Value", new PrimitiveModelValue(123)));

		Assert.ThrowsException<InvalidTemplateModelException>(() => new Template("${ toUpper(Value) }").Render(number));
		Assert.ThrowsException<InvalidTemplateModelException>(() => new Template("${ contains(Value, \"1\") }").Render(number));
	}

	[TestMethod]
	public void Property_Contains_FindsAnyInsertedNonBlankSubstringIgnoringCase()
	{
		var random = new Random(5730);

		for (var i = 0; i < 500; i++)
		{
			var prefix = RandomText(random, 0, 12);
			var search = RandomNonBlankText(random);
			var suffix = RandomText(random, 0, 12);

			Assert.AreEqual(
				true.ToString(),
				Render(prefix + search + suffix, search.ToUpperInvariant()),
				$"value: '{prefix + search + suffix}', search: '{search.ToUpperInvariant()}'");
		}
	}

	[TestMethod]
	public void Property_Contains_BlankSearch_AlwaysFalse()
	{
		var random = new Random(5730);

		for (var i = 0; i < 500; i++)
		{
			var value = RandomText(random, 0, 20);
			var blank = new string(' ', random.Next(0, 4));

			Assert.AreEqual(false.ToString(), Render(value, blank), $"value: '{value}'");
		}
	}

	private static string Render(string value, string search)
	{
		var template = new Template(ContainsTemplate);

		return template.Render(
			new CompositeModelValue(
				new ModelField("Value", value),
				new ModelField("Search", search)));
	}

	private const string Alphabet = "abcxyzABCXYZабвюяАБВЮЯ0129 -_";

	private static string RandomText(Random random, int minLength, int maxLength)
	{
		var length = random.Next(minLength, maxLength + 1);
		return new string(Enumerable.Range(0, length).Select(_ => Alphabet[random.Next(Alphabet.Length)]).ToArray());
	}

	private static string RandomNonBlankText(Random random)
	{
		string text;
		do
		{
			text = RandomText(random, 1, 5);
		}
		while (string.IsNullOrWhiteSpace(text));

		return text;
	}
}
