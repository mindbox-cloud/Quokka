using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
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

			AreFieldCollectionsEquivalent(expected, actual);
			AreMethodCollectionsEquivalent(expected, actual);
		}

		private static void AreFieldCollectionsEquivalent(ICompositeModelDefinition expected, ICompositeModelDefinition actual)
		{
			var expectedFieldList = expected.Fields.OrderBy(kvp => kvp.Key).ToList();
			var actualFieldList = actual.Fields.OrderBy(kvp => kvp.Key).ToList();

			Assert.AreEqual(expectedFieldList.Count, actualFieldList.Count);

			for (int i = 0; i < expectedFieldList.Count; i++)
			{
				var expectedDefinition = expectedFieldList[i];
				var actualDefinition = actualFieldList[i];

				Assert.AreEqual(expectedDefinition.Key, actualDefinition.Key);

				AreModelDefinitionsEquivalent(expectedDefinition.Value, actualDefinition.Value);
			}
		}

		private static void AreMethodCollectionsEquivalent(ICompositeModelDefinition expected, ICompositeModelDefinition actual)
		{
			var expectedMethodList = expected.Methods.ToList();

			Assert.AreEqual(expectedMethodList.Count, actual.Methods.Count);

			foreach (var expectedMethod in expectedMethodList) {
				if (!actual.Methods.TryGetValue(expectedMethod.Key, out IModelDefinition actualDefinition))
					Assert.Fail($"No method call found with expected definition {expectedMethod.Key}");
				
				AreModelDefinitionsEquivalent(expectedMethod.Value, actualDefinition);
			}
		}

		/// <summary>
		/// Asserts that two outputs are equivalent disregarding empty rows and whitespace at the row beginnings and ends.
		/// </summary>
		/// <param name="expected">Expected output</param>
		/// <param name="actual">Actual output</param>
		public static void AreOutputsEquivalent(string expected, string actual)
		{
			Assert.AreEqual(CompactifyOutput(expected), CompactifyOutput(actual));
		}

		/// <summary>
		/// Remove empty lines that are artifacts of rendering 
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		private static string CompactifyOutput(string output)
		{
			return
				string.Concat(
					output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(str => str.Trim()));
		}

		private static void AreModelDefinitionsEquivalent(
			IModelDefinition expected,
			IModelDefinition actual)
		{
			if (expected is IPrimitiveModelDefinition expectedPrimitiveDefinition)
			{
				Assert.IsInstanceOfType(actual, typeof(IPrimitiveModelDefinition));
				var actualPrimitiveDefinition = (IPrimitiveModelDefinition)actual;

				Assert.AreEqual(expectedPrimitiveDefinition.Type, actualPrimitiveDefinition.Type);
			}
			else if (expected is ICompositeModelDefinition expectedCompositeDefinition)
			{
				Assert.IsInstanceOfType(actual, typeof(ICompositeModelDefinition));
				var actualCompositeDefinition = (ICompositeModelDefinition)actual;

				AreCompositeModelDefinitionsEqual(expectedCompositeDefinition, actualCompositeDefinition);
			}
			else if (expected is IArrayModelDefinition expectedArrayDefinition)
			{
				Assert.IsInstanceOfType(actual, typeof(IArrayModelDefinition));
				var actualArrayDefinition = (IArrayModelDefinition)actual;

				Assert.IsNotNull(actualArrayDefinition.ElementModelDefinition);
				AreModelDefinitionsEquivalent(
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
