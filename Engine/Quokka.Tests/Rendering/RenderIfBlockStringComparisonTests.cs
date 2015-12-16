using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class RenderIfBlockStringComparisonTests
	{
		[TestMethod]
		public void Render_IfStringComparison_CaseSensitiveEqual()
		{
			var template = new Template(@"
				@{ if A = ""Margaret"" }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", "Margaret")));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfStringComparison_CaseInsensitiveEqual()
		{
			var template = new Template(@"
				@{ if A = ""mArGaReT"" }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", "Margaret")));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfStringComparison_NonEqual()
		{
			var template = new Template(@"
				@{ if A = ""Margaret"" }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", "Beatrice")));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}
	}
}
