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

namespace Mindbox.Quokka.Tests
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
		public void CreateTemplate_TableRows_UndefinedMethodOnRow_Error()
		{
			new Template(@"
				@{ for row in tableRows(Collection, 3) }
					${ row.DoSomething() }
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
