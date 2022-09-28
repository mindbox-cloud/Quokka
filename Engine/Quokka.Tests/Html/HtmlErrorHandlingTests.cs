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
