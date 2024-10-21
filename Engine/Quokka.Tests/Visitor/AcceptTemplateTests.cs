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