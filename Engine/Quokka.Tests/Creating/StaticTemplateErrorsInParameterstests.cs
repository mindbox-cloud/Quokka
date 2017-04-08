using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class StaticTemplateErrorsInParametersTests
	{
		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void CreateTemplate_LoopVariableHidesGlobalVariable_Error()
		{
			new Template(@"
				@{ for a in Array }
					Nothing.
				@{ end for }

				${ A }
			");
		}
	}
}
