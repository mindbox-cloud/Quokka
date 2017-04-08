using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class HtmlErrorHandlingTests
	{
		[TestMethod]
		public void Html_StaticBlock_NoMatchingRightTag_SyntaxError()
		{
			var templateText = @"<html";
			IList<ITemplateError> errors;
			var htmlTemplate = new DefaultTemplateFactory().TryCreateHtmlTemplate(templateText, out errors);

			Assert.IsNull(htmlTemplate);
			var error = errors.SingleOrDefault();
			Assert.IsNotNull(error);
			Assert.AreEqual(1, error.Location.Line);
		}

		[TestMethod]
		public void Html_StaticBlock_NoMatchingRightTag_InsideIfBlock_SyntaxError()
		{
			// To make sure that errors in HTML subgrammar are located correctly relative to the whole template.
			var templateText = @"
				

				@{ if 5 > 5 } <html @{ end if}


			";

			IList<ITemplateError> errors;
			var htmlTemplate = new DefaultTemplateFactory().TryCreateHtmlTemplate(templateText, out errors);

			Assert.IsNull(htmlTemplate);
			var error = errors.SingleOrDefault();
			Assert.IsNotNull(error);
			Assert.AreEqual(4, error.Location.Line);
			Assert.AreEqual(24, error.Location.Column);
		}
	}
}
