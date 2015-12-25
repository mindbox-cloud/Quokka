using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class StaticTemplateErrorTests
	{
		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void CreateTemplate_InvalidFunctionArgumentCount_TooMany_Error()
		{
			new Template(@"${ toLower(""valid argument"", ""excessive argument"") }");
		}

		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void CreateTemplate_InvalidFunctionArgumentCount_TooLittle_Error()
		{
			new Template(@"${ toLower() }");
		}
	}
}
