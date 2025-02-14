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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests;

[TestClass]
public class RendeIsEmptyFunctionTests
{
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

	[TestMethod]
	public void Render_IsEmptyAndIfCombination_WorksWithNullValue()
	{
		var template = new Template("${ if( isEmpty(productionFlag), \"test.example.com\", \"example.com\") }");

		var result = template.Render(
			new CompositeModelValue(
				new ModelField("productionFlag", new PrimitiveModelValue(null))));

		Assert.AreEqual("test.example.com", result);
	}

	[TestMethod]
	public void Render_IsEmptyAndIfBlock_WorksWithNonStringPrimitive()
	{
		var template = new Template(
			@"
				@{ if isEmpty(Price) }
					empty
				@{ else }
					formatDecimal(Price, "".00"")
				@{ end if }
			");

		var result = template.Render(
			new CompositeModelValue(
				new ModelField("Price", new PrimitiveModelValue(null))));

		var expected = @"
				
					empty
				
			";
		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void Render_IsEmpty_NonEmptyArray_False()
	{
		var template = new Template(@"${ if(isEmpty(Values), 'Empty', 'Not empty') }");

		var result = template.Render(
			new CompositeModelValue(
				new ModelField(
					"Values",
					new ArrayModelValue(new PrimitiveModelValue("value")))));

		var expected = @"Not empty";
		
		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void Render_IsEmpty_EmptyArray_True()
	{
		var template = new Template(@"${ if(isEmpty(Values), 'Empty', 'Not empty') }");

		var result = template.Render(
			new CompositeModelValue(
				new ModelField(
					"Values",
					new ArrayModelValue())));

		var expected = @"Empty";
		
		Assert.AreEqual(expected, result);
	}
}