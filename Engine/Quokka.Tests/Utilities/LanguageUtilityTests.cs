using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
    [TestClass]
	public class LanguageUtilityTests
	{
		[TestMethod]
		public void Test()
		{
			Assert.AreEqual("майка",
				LanguageUtility.GetQuantityForm(
					1,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("майки",
				LanguageUtility.GetQuantityForm(
					2,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("майки",
				LanguageUtility.GetQuantityForm(
					3,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("майки",
				LanguageUtility.GetQuantityForm(
					4,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					5,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					6,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					7,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					8,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					9,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					11,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					12,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					13,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					14,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					15,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					16,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					17,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					18,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					19,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					20,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("майка",
				LanguageUtility.GetQuantityForm(
					21,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("майки",
				LanguageUtility.GetQuantityForm(
					22,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("майки",
				LanguageUtility.GetQuantityForm(
					23,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("майки",
				LanguageUtility.GetQuantityForm(
					24,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					25,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					26,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					27,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					28,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					29,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					30,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("майка",
				LanguageUtility.GetQuantityForm(
					28473641,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("майки",
				LanguageUtility.GetQuantityForm(
					28473643,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					28473645,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					28473612,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					0,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("майка",
				LanguageUtility.GetQuantityForm(
					-1,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("майки",
				LanguageUtility.GetQuantityForm(
					-2,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					-5,
					"майка",
					"майки",
					"маек"));

			Assert.AreEqual("маек",
				LanguageUtility.GetQuantityForm(
					int.MinValue, // -2,147,483,648
					"майка",
					"майки",
					"маек"));
		}
	}
}