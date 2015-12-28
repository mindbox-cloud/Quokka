using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class StaticErrorsOnTableRowsTests
	{
		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void CreateTemplate_TableRows_TreatingRowAsPrimitive_Error()
		{
			new Template(@"
				@{ for row in tableRows(Collection, 3) }
					${ row }
				@{ end for }
			");
		}


		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void CreateTemplate_TableRows_TreatingRowAsArray_Error()
		{
			new Template(@"
				@{ for row in tableRows(Collection, 3) }
					@{ for cell in row }

					@{ end for }
				@{ end for }
			");
		}

		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void CreateTemplate_TableRows_UndefinedFieldOnRow_Error()
		{
			new Template(@"
				@{ for row in tableRows(Collection, 3) }
					${ row.Something }
				@{ end for }
			");
		}

		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void CreateTemplate_TableRows_TreatingCellsAsPrimitive_Error()
		{
			new Template(@"
				@{ for row in tableRows(Collection, 3) }
					${ row.Cells }
				@{ end for }
			");
		}

		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void CreateTemplate_TableRows_UndefiedFieldOnCell_Error()
		{
			new Template(@"
				@{ for row in tableRows(Collection, 3) }
					@{ for cell in row.Cells }
						${ cell.Something }
					@{ end for }
				@{ end for }
			");
		}
	}
}
