using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	internal static class TemplateAssert
	{
		/// <summary>
		/// Check if parameter definition collections are equal (deep checking including fields and collection element fields).
		/// Order matters (parameters returned by Template are sorted alphabetically).
		/// </summary>
		/// <param name="expected">Expected parameter definitions</param>
		/// <param name="actual">Actual parameter definitions (those returned by the template).</param>
		public static void AreCompositeModelDefinitionsEqual(
			ICompositeModelDefinition expected,
			ICompositeModelDefinition actual)
		{
			if (expected == null)
				throw new ArgumentNullException(nameof(expected));

			Assert.IsNotNull(actual);

			var expectedFieldList = expected.Fields.OrderBy(kvp => kvp.Key).ToList();
			var actualFieldList = expected.Fields.OrderBy(kvp => kvp.Key).ToList();

			Assert.AreEqual(expectedFieldList.Count, actualFieldList.Count);

			for (int i = 0; i < expectedFieldList.Count; i++)
			{
				var expectedDefinition = expectedFieldList[i];
				var actualDefinition = actualFieldList[i];

				Assert.AreEqual(expectedDefinition.Key, actualDefinition.Key);

				AreModelDefinitionsEqual(expectedDefinition.Value, actualDefinition.Value);
			}
		}

		private static void AreModelDefinitionsEqual(
			IModelDefinition expected,
			IModelDefinition actual)
		{
			if (expected is IPrimitiveModelDefinition)
			{
				var expectedPrimitiveDefinition = (IPrimitiveModelDefinition)expected;
				Assert.IsInstanceOfType(actual, typeof(IPrimitiveModelDefinition));
				var actualPrimitiveDefinition = (IPrimitiveModelDefinition)actual;

				Assert.AreEqual(expectedPrimitiveDefinition.Type, actualPrimitiveDefinition.Type);
			}
			else if (expected is ICompositeModelDefinition)
			{
				var expectedCompositeDefinition = (ICompositeModelDefinition)expected;
				Assert.IsInstanceOfType(actual, typeof(ICompositeModelDefinition));
				var actualCompositeDefinition = (ICompositeModelDefinition)actual;

				AreCompositeModelDefinitionsEqual(expectedCompositeDefinition, actualCompositeDefinition);
			}
			else if (expected is IArrayModelDefinition)
			{
				var expectedArrayDefinition = (IArrayModelDefinition)expected;
				Assert.IsInstanceOfType(actual, typeof(IArrayModelDefinition));
				var actualArrayDefinition = (IArrayModelDefinition)actual;

				Assert.IsNotNull(actualArrayDefinition.ElementModelDefinition);
				AreModelDefinitionsEqual(
					expectedArrayDefinition.ElementModelDefinition,
					actualArrayDefinition.ElementModelDefinition);
			}
			else
			{
				throw new InvalidOperationException("Unexpected type");
			}
		}
	}
}
