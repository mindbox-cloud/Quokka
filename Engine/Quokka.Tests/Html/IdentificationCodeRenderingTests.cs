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
	public class IdentificationCodeRenderingTests
	{
		[TestMethod]
		public void Html_IdentificationCode_LogicLess_EndBody()
		{
			var htmlTemplate = new HtmlTemplate(@"
				<html>
				<body>
				Hello
				</body>
				</html>
			");

			var expected = @"
				<html>
				<body>
				Hello
				<img src=track.php></body>
				</html>
			";

			var actual = htmlTemplate.Render(new CompositeModelValue(), null, "<img src=track.php>");

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Html_IdentificationCode_LogicLess_OnlyEndBody()
		{
			var htmlTemplate = new HtmlTemplate(@"</body>");

			var expected = @"<img src=track.php></body>";

			var actual = htmlTemplate.Render(new CompositeModelValue(), null, "<img src=track.php>");

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Html_IdentificationCode_EndBodyInsideIfBlock_ConditionTrue()
		{
			var htmlTemplate = new HtmlTemplate(@"
				<html>				
				<body>
				Hello
				@{ if A }
				</body>
				@{ end if }
				</html>
			");

			var expected = @"
				<html>				
				<body>
				Hello
				
				<img src=track.php></body>
				
				</html>
			";

			var actual = htmlTemplate.Render(
				new CompositeModelValue(
					new ModelField("A", new PrimitiveModelValue(true))),
				null,
				"<img src=track.php>");

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Html_IdentificationCode_EndBodyInsideIfBlock_ConditionFalse()
		{
			var htmlTemplate = new HtmlTemplate(@"
				<html>				
				<body>
				Hello
				@{ if A }
				</body>
				@{ end if }
				</html>
			");

			var expected = @"
				<html>				
				<body>
				Hello
				
				</html>
			<img src=track.php>";

			var actual = htmlTemplate.Render(
				new CompositeModelValue(
					new ModelField("A", new PrimitiveModelValue(false))),
				null,
				"<img src=track.php>");

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Html_IdentificationCode_EndBodyInsideForBlock_OnlyOneCode()
		{
			var htmlTemplate = new HtmlTemplate(@"
				<html>				
				<body>
				Hello
				@{ for element in Elements}
				</body>
				@{ end for }
				</html>
			");

			var expected = @"
				<html>				
				<body>
				Hello
				
				<img src=track.php></body>
				
				</body>
				
				</body>
				
				</html>
			";

			var actual = htmlTemplate.Render(
				new CompositeModelValue(
					new ModelField("Elements", new ArrayModelValue(
						new PrimitiveModelValue(1),
						new PrimitiveModelValue(2),
						new PrimitiveModelValue(3)))),
				null,
				"<img src=track.php>");

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Html_IdentificationCode_NoEndBody()
		{
			var htmlTemplate = new HtmlTemplate(@"
				<html>
				</html>
			");

			var expected = @"
				<html>
				</html>
			<img src=track.php>";

			var actual = htmlTemplate.Render(new CompositeModelValue(), null, "<img src=track.php>");

			Assert.AreEqual(expected, actual);
		}
	}
}
