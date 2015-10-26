using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Quokka.Html;

namespace Quokka.Tests.Html
{
	[TestClass]
	public class HtmlReferenceDiscoveryTests
	{
		[TestMethod]
		public void Html_ReferenceDiscovery_NoReferences()
		{
			var template = new HtmlTemplate("No links here");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEqual(
				new List<Reference>(),
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_AHref_DoubleQuoted()
		{
			var template = new HtmlTemplate("<a href=\"http://example.com\">Test</a>");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEqual(
				new[]
				{
					new Reference("http://example.com", null, true),
				},
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_AHref_SingleQuoted()
		{
			var template = new HtmlTemplate("<a href='http://example.com'>Test</a>");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEqual(
				new[]
				{
					new Reference("http://example.com", null, true),
				},
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_AreaHref_OpeningDoubleQuoted()
		{
			var template = new HtmlTemplate("<area href=\"http://example.com\">");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEqual(
				new[]
				{
					new Reference("http://example.com", null, true),
				},
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_AreaHref_OpeningSingleQuoted()
		{
			var template = new HtmlTemplate("<area href='http://example.com'>");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEqual(
				new[]
				{
					new Reference("http://example.com", null, true),
				},
				references);
		}
		
		[TestMethod]
		public void Html_ReferenceDiscovery_AreaHref_SelfClosingDoubleQuoted()
		{
			var template = new HtmlTemplate("<area href=\"http://example.com\" />");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEqual(
				new[]
				{
					new Reference("http://example.com", null, true),
				},
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_AreaHref_SelfClosingSingleQuoted()
		{
			var template = new HtmlTemplate("<area href='http://example.com' />");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEqual(
				new[]
				{
					new Reference("http://example.com", null, true),
				},
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_AHrefs_Multuple()
		{
			var template = new HtmlTemplate(@"
				<a href='http://example.com'>Test</a>
				<a href='http://google.com'>Test</a>				
			");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEqual(
				new[]
				{
					new Reference("http://example.com", null, true),
					new Reference("http://google.com", null, true)
				},
				references);
		}
	}
}
