using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
    public class ModelDiscoveryErrorsTests
    {
	    [TestMethod]
		[Ignore]
	    public void ModelDiscovery_InconsistentVariableUsages_Error()
	    {
		    new Template(@"
				@{ for x in A }
					${ x.Z + 5 }
					${ x.Z and y }
				@{ end for }
			");
	    }
    }
}
