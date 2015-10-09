using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class ApplyParameterOutputTests
	{
		[TestMethod]
		public void Apply_SingleStringParameter()
		{
			var template = new Template("${ Name }");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Name", "Angelina")));

			Assert.AreEqual("Angelina", result);
		}

		[TestMethod]
		public void Apply_SingleIntegerParameter()
		{
			var template = new Template("${ LetterId }");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("LetterId", 43)));

			Assert.AreEqual("43", result);
		}

		[TestMethod]
		public void Apply_MultipleStringParameters()
		{
			var template = new Template("${ FirstName } ${ LastName }");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("FirstName", "Winona"),
					new ModelField("LastName", "Ryder")));

			Assert.AreEqual("Winona Ryder", result);
		}

		[TestMethod]
		public void Apply_StringParameterInPlainText()
		{
			var template = new Template("Hello, ${ FirstName }!");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("FirstName", "Kate")));

			Assert.AreEqual("Hello, Kate!", result);
		}

		[TestMethod]
		public void Apply_SecondLevelParameterMember()
		{
			var template = new Template("${ Customer.Email }");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Customer",
						new CompositeModelValue(
							new ModelField("Email", "jessica@example.com")))));

			Assert.AreEqual("jessica@example.com", result);
		}

		[TestMethod]
		public void Apply_NthLevelParameterMember()
		{
			var template = new Template("${ Customer.Data.Contacts.Email }");

			var result = template.Apply(
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
		public void Apply_TwoSecondLevelParameterMembers()
		{
			var template = new Template("${ Customer.Email }, ${ Customer.MobilePhone }");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Customer",
						new CompositeModelValue(
							new ModelField("Email", "jessica@example.com"),
							new ModelField("MobilePhone", "79990000123")))));

			Assert.AreEqual("jessica@example.com, 79990000123", result);
		}

		[TestMethod]
		public void Apply_ParameterCaseInsensitivity()
		{
			var template = new Template("${ ProductModel }");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("pRoDuCtmOdEl", "ES-335")));

			Assert.AreEqual("ES-335", result);
		}
	}
}
