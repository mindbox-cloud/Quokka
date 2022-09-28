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

namespace Mindbox.Quokka
{
	[TestClass]
    public class StaticTemplateErrorsTests
    {
	    [TestMethod]
	    public void CreateTemplate_NonConstantMethodValue_SemanticError()
	    {
		    new DefaultTemplateFactory()
			    .TryCreateTemplate(
				    "${ Root.GetValue(A) }",
				    out IList<ITemplateError> errors);

		    Assert.AreEqual(1, errors.Count);
		}

	    [TestMethod]
	    public void CreateTemplate_FieldAndMethodNameConflict_SemanticError()
	    {
		    new DefaultTemplateFactory()
			    .TryCreateTemplate(@"
					${ Root.Property }
					${ Root.Property('5') }
				",
				out IList<ITemplateError> errors);

		    Assert.AreEqual(1, errors.Count);
	    }

		[TestMethod]
		public void CreateTemplate_ErrorCharacterIsAUnicodeCodePoint_CorrectException()
		{
			Assert.ThrowsException<TemplateContainsErrorsException>(() => new Template(@"@{if💝"));
		}
	}
}
