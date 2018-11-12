using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class StaticAssignmentErrorsTests
	{
		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void Static_AssignmentBlock_VariableUsageBeforeAssignment()
		{
			new Template(@"
				${ a }
				@{ set a = 2 }");
		}
		
		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void Static_AssignmentBlock_InconsistentVariableUsages()
		{
			new Template(@"
					@{ set a = 5 }
					@{ set a = 'string' }");
		}

		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void Static_AssignmentBlock_OutOfScopeVariableUsage()
		{
			new Template(@"
				@{ for x in items }
					@{ set a = 5 }
				@{ end for }

				${ a }");
		}
	}
}
