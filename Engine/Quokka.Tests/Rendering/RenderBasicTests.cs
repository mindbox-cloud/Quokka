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

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class RenderBasicTests
	{
		[TestMethod]
		public void Render_SingleConstantBlock()
		{
			var template = new Template("Happy new year!");

			Assert.AreEqual(
				"Happy new year!",
				template.Render(new CompositeModelValue()));
		}

		[TestMethod]
		public void Render_EmptyString()
		{
			var template = new Template("");

			Assert.AreEqual(
				"",
				template.Render(new CompositeModelValue()));
		}

		[TestMethod]
		public void Render_Comment()
		{
			var template = new Template("Visible @{* Not Visible *} Visible");

			Assert.AreEqual(
				"Visible  Visible",
				template.Render(new CompositeModelValue()));
		}

		[TestMethod]
		public void Render_StringConstantOutput()
		{
			var template = new Template("${ \"Constant value  \" }");

			Assert.AreEqual(
				"Constant value  ",
				template.Render(new CompositeModelValue()));
		}

		[TestMethod]
		public void Render_BooleanTrueOutput()
		{
			var template = new Template("${ A or A }");

			Assert.AreEqual(
				"True",
				template.Render(
					new CompositeModelValue(
						new ModelField("A", true))));
		}

		[TestMethod]
		public void Render_BooleanFalseOutput()
		{
			var template = new Template("${ not A }");

			Assert.AreEqual(
				"False",
				template.Render(
					new CompositeModelValue(
						new ModelField("A", true))));
		}

		[TestMethod]
		public void Render_DoubleQuotedString()
		{
			var template = new Template(@"${ ""Some 'value'"" }");

			Assert.AreEqual(
				"Some 'value'",
				template.Render(
					new CompositeModelValue()));
		}

		[TestMethod]
		public void Render_SingleQuotedString()
		{
			var template = new Template(@"${ 'Some ""value""' }");

			Assert.AreEqual(
				"Some \"value\"",
				template.Render(
					new CompositeModelValue()));
		}
	}
}
