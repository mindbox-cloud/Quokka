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

namespace Mindbox.Quokka.Tests;

[TestClass]
public class PreHeaderRenderingTests
{
    private const string PreHeaderTestValue = "<text>Aboba</text>";

    [TestMethod]
    public void Html_PreHeader_LogicLess_ValidBody_RenderedBeforeBody()
    {
        var htmlTemplate = new HtmlTemplate(
            @"
				<html>
				<body>
				<div>
				Hello
				</div>
				</body>
				</html>
			");


		var expected = @$"
				<html>
				<body>{PreHeaderTestValue}
				<div>
				Hello
				</div>
				</body>
				</html>
			";

        var actual = htmlTemplate.Render(
            new CompositeModelValue(),
            null,
            preHeader: PreHeaderTestValue);

        Assert.AreEqual(expected, actual);
    }
	
	[TestMethod]
	public void Html_PreHeader_LogicLess_OnlyOpeningBody_RenderedBeforeBody()
	{
		var htmlTemplate = new HtmlTemplate(@"<body>Hello");

		var expected = @$"<body>{PreHeaderTestValue}Hello";

		var actual = htmlTemplate.Render(new CompositeModelValue(), null, preHeader: PreHeaderTestValue);

		Assert.AreEqual(expected, actual);
	}

    [TestMethod]
    public void Html_PreHeader_OpenBodyInsideForBlock_OnlyOnePreHeader()
    {
        var htmlTemplate = new HtmlTemplate(
            @"
				<html>		
				@{ for element in Elements}		
				<body>
				@{ end for }
				Hello
				</body>
				</html>
			");

        var expected = $@"
				<html>		
						
				<body>{PreHeaderTestValue}
						
				<body>
						
				<body>
				
				Hello
				</body>
				</html>
			";

        var actual = htmlTemplate.Render(
            new CompositeModelValue(
                new ModelField(
                    "Elements",
                    new ArrayModelValue(
                        new PrimitiveModelValue(1),
                        new PrimitiveModelValue(2),
                        new PrimitiveModelValue(3)))),
            null,
			preHeader: PreHeaderTestValue);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Html_PreHeader_NoOpenBodyRenderedInTheBeginning()
    {
        var htmlTemplate = new HtmlTemplate(
            @"
				<html>
				</html>
			");

        var expected = $@"{PreHeaderTestValue}
				<html>
				</html>
			";

        var actual = htmlTemplate.Render(
			new CompositeModelValue(),
			null,
			preHeader: PreHeaderTestValue);

        Assert.AreEqual(expected, actual);
    }
	
	[TestMethod]
	public void NonHtml_PreHeader_RenderedInTheBeginning()
	{
		var htmlTemplate = new HtmlTemplate(
			@"Just a string");

		var expected = $@"{PreHeaderTestValue}Just a string";

		var actual = htmlTemplate.Render(
			new CompositeModelValue(),
			null,
			preHeader: PreHeaderTestValue);

		Assert.AreEqual(expected, actual);
	}
}