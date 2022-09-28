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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mindbox.Quokka.Html;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class RedirectLinkProcessingTests
	{
		[TestMethod]
		public void Html_RedirectLinkProcessing_NullProcessor()
		{
			var htmlTemplate = new HtmlTemplate(@"<a href=""http://example.com"">link</a>");
			var html = htmlTemplate.Render(
				new CompositeModelValue(),
				null);

			Assert.AreEqual(
				@"<a href=""http://example.com"">link</a>",
				html);
		}

		[TestMethod]
		public void Html_RedirectLinkProcessing_SimpleStringProcessing()
		{
			var htmlTemplate = new HtmlTemplate(@"<a href=""http://example.com"">link</a>");
			var html = htmlTemplate.Render(
				new CompositeModelValue(),
				(uniqueKey, redirectUrl) => redirectUrl.ToUpper());

			Assert.AreEqual(
				@"<a href=""HTTP://EXAMPLE.COM"">link</a>",
				html);
		}

		[TestMethod]
		public void Html_RedirectLinkProcessing_LinkWithMultipleParameters()
		{
			var htmlTemplate = new HtmlTemplate(@"<a href=""${ scheme }://${ domain }"">link</a>");
			var html = htmlTemplate.Render(
				new CompositeModelValue(
					new ModelField("scheme", "https"),
					new ModelField("domain", "example.com")),
				(uniqueKey, redirectUrl) => redirectUrl.ToUpper());

			Assert.AreEqual(
				@"<a href=""HTTPS://EXAMPLE.COM"">link</a>",
				html);
		}
	}
}
