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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Snapshooter.MSTest;

namespace Mindbox.Quokka.Tests;

[TestClass]
public class AcceptTemplateTests
{
    [TestMethod]
    public void EnsureAllVisitMethodsAreInvokedForTemplate()
    {
		var templateText = """
							${ Math.Min(32, 16) + Math.Square(6) + Math.DecimalPart() }
							${ (24 + 3) + (5 * 25)/7 - (6 * 7 / 8*9) * (242) + A.Value * B.Value.Length }
							@{ set x = 353 + 255 }${ x }
							@{ if IsRed }
								Red
							@{ else if IsGreen }
								Green
							@{ else if IsBlue }
								Blue
							@{ else if IsYellow }
								Yellow
							@{ end if }
							@{ for item in Collection }
								List element
							@{ end for }
							""";

		var visitor = new TestTreeVisitor();
        var template = new Template(templateText);
		

        template.Accept(visitor);
		
		
        Snapshot.Match(visitor.VisitedNodes);
    }
}