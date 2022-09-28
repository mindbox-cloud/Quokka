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

using Mindbox.Quokka.Html;

namespace Mindbox.Quokka.Tests.Html
{
	[TestClass]
	public class HtmlTemplateBasicTests
	{
		[TestMethod]
		public void Html_PlainTextWithParameterOutput()
		{
			var template = new HtmlTemplate("Hello, ${ Name }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Name", "Angelina")));

			Assert.AreEqual("Hello, Angelina", result);
		}

		[TestMethod]
		public void Html_SingleAhref_WithoutOutputBlocks()
		{
			var template = new HtmlTemplate("<a href=\"http://example.com\">");

			var result = template.Render(new CompositeModelValue());

			Assert.AreEqual("<a href=\"http://example.com\">", result);
		}

		[TestMethod]
		public void Html_SingleAhref_WithMultipleOutputBlocks()
		{
			var template = new HtmlTemplate("<a href=\"${ \"https\" }://example.com/${ 4 + 13 }\">");

			var result = template.Render(new CompositeModelValue());

			Assert.AreEqual("<a href=\"https://example.com/17\">", result);
		}

		[TestMethod]
		public void Html_SingleAhref_SingleOutputBlock()
		{
			var template = new HtmlTemplate("<a href=\"${ 1 * 2 * 3 * 4 * 5 }\">");

			var result = template.Render(new CompositeModelValue());

			Assert.AreEqual("<a href=\"120\">", result);
		}
	}
}
