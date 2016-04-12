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
		
		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void CreateTemplate_InvalidFunctionTypeInForLoop_Error()
		{
			new Template(@"
				@{ for a in toLower(""string"") }
				
				@{ end for }
			");
		}

		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void CreateTemplate_UnknownFunctionName_Error()
		{
			new Template(@"
				@{ if nonExistingFunction(""value"") }
					Something
				@{ end if }
			");
		}

		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void CreateTemplate_InvalidStaticArgumentType_Error()
		{
			new Template("${ formatDecimal(5, 0) }");
		}
	}
}
