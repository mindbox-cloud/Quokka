using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class IsConstantTests
	{
		[TestMethod]
		public void TemplateIsConstant_EmptyTemplate_True()
		{
			var template = new Template("");
			Assert.IsTrue(template.IsConstant);
		}

		[TestMethod]
		public void TemplateIsConstant_SingleConstantTemplate_True()
		{
			var template = new Template("Hello");
			Assert.IsTrue(template.IsConstant);
		}

		[TestMethod]
		public void TemplateIsConstant_OutputBlock_False()
		{
			var template = new Template("${ Name }");
			Assert.IsFalse(template.IsConstant);
		}

		[TestMethod]
		public void TemplateIsConstant_OutputBlockInsideConstants_False()
		{
			var template = new Template("Hello, ${ Name }!");
			Assert.IsFalse(template.IsConstant);
		}
	}
}
