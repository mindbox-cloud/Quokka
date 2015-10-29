using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Quokka.Html;

namespace Quokka.Tests
{
	internal static class ReferencesAssert
	{
		public static void AreCollectionsEquivalent(IEnumerable<Reference> expected, IReadOnlyList<Reference> actual)
		{
			var expectedList = expected.ToList();

			Assert.IsNotNull(actual);
			Assert.AreEqual(expectedList.Count, actual.Count);

			for (int i = 0; i < expectedList.Count; i++)
			{
				var expectedElement = expectedList[i];
				var actualElement = actual[i];

				Assert.AreEqual(expectedElement.RedirectUrl, actualElement.RedirectUrl);
				Assert.AreEqual(expectedElement.Name, actualElement.Name);
				Assert.AreEqual(expectedElement.IsConstant, actualElement.IsConstant);
			}
		}
	}
}
