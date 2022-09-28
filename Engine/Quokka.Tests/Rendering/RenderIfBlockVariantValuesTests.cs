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

using Mindbox.Quokka.Tests;

namespace Mindbox.Quokka
{
	[TestClass]
    public class RenderIfBlockVariantValuesTests
    {
		[TestMethod]
	    public void Render_IfCondition_MethodResult()
	    {
			var template = new Template(@"
				@{ if Recipient.GetValue('IsMale') }
					Male
				@{ end if }");

		    var result = template.Render(
			    new CompositeModelValue(
				    new ModelField(
					    "Recipient",
					    new CompositeModelValue(
						    new ModelMethod("GetValue", new object[] { "IsMale" }, true)))));

		    var expected = @"
				Male
			";

		    TemplateAssert.AreOutputsEquivalent(expected, result);
		}
    }
}
