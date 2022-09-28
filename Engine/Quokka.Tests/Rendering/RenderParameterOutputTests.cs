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
	public class RenderParameterOutputTests
	{
		[TestMethod]
		public void Render_SingleStringParameter()
		{
			var template = new Template("${ Name }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Name", "Angelina")));

			Assert.AreEqual("Angelina", result);
		}

		[TestMethod]
		public void Render_SingleIntegerParameter()
		{
			var template = new Template("${ LetterId }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("LetterId", 43)));

			Assert.AreEqual("43", result);
		}

		[TestMethod]
		public void Render_MultipleStringParameters()
		{
			var template = new Template("${ FirstName } ${ LastName }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("FirstName", "Winona"),
					new ModelField("LastName", "Ryder")));

			Assert.AreEqual("Winona Ryder", result);
		}

		[TestMethod]
		public void Render_StringParameterInPlainText()
		{
			var template = new Template("Hello, ${ FirstName }!");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("FirstName", "Kate")));

			Assert.AreEqual("Hello, Kate!", result);
		}

		[TestMethod]
		public void Render_SecondLevelParameterMember()
		{
			var template = new Template("${ Customer.Email }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Customer",
						new CompositeModelValue(
							new ModelField("Email", "jessica@example.com")))));

			Assert.AreEqual("jessica@example.com", result);
		}

		[TestMethod]
		public void Render_NthLevelParameterMember()
		{
			var template = new Template("${ Customer.Data.Contacts.Email }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Customer",
						new CompositeModelValue(
							new ModelField("Data",
								new CompositeModelValue(
									new ModelField("Contacts",
										new CompositeModelValue(
											new ModelField("Email", "jessica@example.com")))))))));

			Assert.AreEqual("jessica@example.com", result);
		}

		[TestMethod]
		public void Render_TwoSecondLevelParameterMembers()
		{
			var template = new Template("${ Customer.Email }, ${ Customer.MobilePhone }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Customer",
						new CompositeModelValue(
							new ModelField("Email", "jessica@example.com"),
							new ModelField("MobilePhone", "79990000123")))));

			Assert.AreEqual("jessica@example.com, 79990000123", result);
		}

		[TestMethod]
		public void Render_ParameterCaseInsensitivity()
		{
			var template = new Template("${ ProductModel }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("pRoDuCtmOdEl", "ES-335")));

			Assert.AreEqual("ES-335", result);
		}

		[TestMethod]
		public void Render_ParametersWithUnderscores()
		{
			var template = new Template(@"
				${ _prefix }
				${ in_fix }
				${ suffix_ }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("_prefix", "Hope"),
					new ModelField("in_fix", "Empire"),
					new ModelField("suffix_", "Return")));

			var expected = @"
				Hope
				Empire
				Return
			";

			Assert.AreEqual(expected, result);
		}
	}
}
