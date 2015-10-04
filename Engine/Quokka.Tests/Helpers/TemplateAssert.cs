using System;
using System.Collections.Generic;
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
		public static void AreParameterDefinitionsEqual(
			IEnumerable<IParameterDefinition> expected,
			IList<IParameterDefinition> actual)
		{
			if (expected == null)
				throw new ArgumentNullException(nameof(expected));

			Assert.IsNotNull(actual);

			var expectedList = expected.ToList();
			var actualList = actual.ToList();

			Assert.AreEqual(expectedList.Count, actualList.Count);

			for (int i = 0; i < expectedList.Count; i++)
			{
				var expectedDefinition = expectedList[i];
				var actualDefinition = actualList[i];

				Assert.AreEqual(expectedDefinition.Name, actualDefinition.Name);
				Assert.AreEqual(expectedDefinition.Type, actualDefinition.Type);

				switch (expectedDefinition.Type)
				{
					case VariableType.Composite:
						var expectedCompositeDefinition = (ICompositeParameterDefinition)expectedDefinition;

						Assert.IsInstanceOfType(actualDefinition, typeof(ICompositeParameterDefinition));
						var actualCompositeDefinition = (ICompositeParameterDefinition)actualDefinition;

						AreParameterDefinitionsEqual(expectedCompositeDefinition.Fields, actualCompositeDefinition.Fields);
						break;

					case VariableType.Array:
						var expectedArrayDefinition = (IArrayParameterDefinition)expectedDefinition;

						Assert.IsInstanceOfType(actualDefinition, typeof(IArrayParameterDefinition));
						var actualArrayDefinition = (IArrayParameterDefinition)actualDefinition;

						AreParameterDefinitionsEqual(expectedArrayDefinition.ElementFields, actualArrayDefinition.ElementFields);
						break;
				}
			}
		}
	}
}
