﻿using System;
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

			ReferencesAssert.AreCollectionsEquivalent(
				new List<Reference>(),
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_AHref_DoubleQuoted()
		{
			var template = new HtmlTemplate("<a href=\"http://example.com\">Test</a>");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEquivalent(
				new[]
				{
					new Reference("http://example.com", null, Guid.NewGuid(), true),
				},
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_AHref_SingleQuoted()
		{
			var template = new HtmlTemplate("<a href='http://example.com'>Test</a>");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEquivalent(
				new[]
				{
					new Reference("http://example.com", null, Guid.NewGuid(), true),
				},
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_AHref_UnQuoted()
		{
			var template = new HtmlTemplate("<a href=http://example.com>Test</a>");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEquivalent(
				new[]
				{
					new Reference("http://example.com", null, Guid.NewGuid(), true),
				},
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_AreaHref_OpeningDoubleQuoted()
		{
			var template = new HtmlTemplate("<area href=\"http://example.com\">");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEquivalent(
				new[]
				{
					new Reference("http://example.com", null, Guid.NewGuid(), true),
				},
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_AreaHref_OpeningSingleQuoted()
		{
			var template = new HtmlTemplate("<area href='http://example.com'>");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEquivalent(
				new[]
				{
					new Reference("http://example.com", null, Guid.NewGuid(), true),
				},
				references);
		}
		
		[TestMethod]
		public void Html_ReferenceDiscovery_AreaHref_SelfClosingDoubleQuoted()
		{
			var template = new HtmlTemplate("<area href=\"http://example.com\" />");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEquivalent(
				new[]
				{
					new Reference("http://example.com", null, Guid.NewGuid(), true),
				},
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_AreaHref_SelfClosingSingleQuoted()
		{
			var template = new HtmlTemplate("<area href='http://example.com' />");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEquivalent(
				new[]
				{
					new Reference("http://example.com", null, Guid.NewGuid(), true),
				},
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_AHrefs_Multiple()
		{
			var template = new HtmlTemplate(@"
				<a href='http://example.com'>Test</a>
				<a href='http://google.com'>Test</a>				
			");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEquivalent(
				new[]
				{
					new Reference("http://example.com", null, Guid.NewGuid(), true),
					new Reference("http://google.com", null, Guid.NewGuid(), true)
				},
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_LinksInsideIfConditions()
		{
			var template = new HtmlTemplate(@"
				@{ if A > B }
					<a href=""http://somelink.example.com""></a>
				@{ else if C > D }					
					<area href=""http://somelink2.example.com""></a>
				@{ else }
					<A href='http://somelink3.example.com'></a>
				@{ end if }				
			");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEquivalent(
				new[]
				{
					new Reference("http://somelink.example.com", null, Guid.NewGuid(), true),
					new Reference("http://somelink2.example.com", null, Guid.NewGuid(), true),
					new Reference("http://somelink3.example.com", null, Guid.NewGuid(), true)
				},
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_LinksInsideForLoop()
		{
			var template = new HtmlTemplate(@"
				@{ for a in Elements }
					<A href=""http://quokka.example.com"">
				@{ end for }			
			");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEquivalent(
				new[]
				{
					new Reference("http://quokka.example.com", null, Guid.NewGuid(), true)
				},
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_LinksInsideComment_NotFound()
		{
			var template = new HtmlTemplate(@"
				@{*
					<A href=""http://quokka.example.com"">
				*}			
			");
			var references = template.GetReferences();

			Assert.IsFalse(references.Any());
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_DeepNestedLinks()
		{
			var template = new HtmlTemplate(@"
				@{ for a in Elements }
					@{ if a.Boolean }
						${ a.Boolean}
							<a href=""http://example.com"">
								<a href=""http://inner.example.com""></a>
							</a>
						${ 5 }
					@{ end if }
				@{ end for }			
			");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEquivalent(
				new[]
				{
					new Reference("http://example.com", null, Guid.NewGuid(), true),
					new Reference("http://inner.example.com", null, Guid.NewGuid(), true)
				},
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_ParameterOutputInDoubleQuotes()
		{
			var template = new HtmlTemplate(@"
				<a href=""${ URL }"">			
			");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEquivalent(
				new[]
				{
					new Reference("${ URL }", null, Guid.NewGuid(), false)
				},
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_ParameterOutputInSingleQuotes()
		{
			var template = new HtmlTemplate(@"
				<a href='${ URL }'>			
			");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEquivalent(
				new[]
				{
					new Reference("${ URL }", null, Guid.NewGuid(), false)
				},
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_MultipleParametersWithComplexStructure()
		{
			var template = new HtmlTemplate(@"
				<a href=""${ Scheme }://${ Domain | ReplaceIfEmpty(AnotherDomain)}  "">		
			");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEquivalent(
				new[]
				{
					new Reference("${ Scheme }://${ Domain | ReplaceIfEmpty(AnotherDomain)}", null, Guid.NewGuid(), false)
				},
				references);
		}

		[TestMethod]
		public void Html_ReferenceDiscovery_SpacesAreTrimmed()
		{
			var template = new HtmlTemplate("<a href=\"  http://example.com			 \">Test</a>");
			var references = template.GetReferences();

			ReferencesAssert.AreCollectionsEquivalent(
				new[]
				{
					new Reference("http://example.com", null, Guid.NewGuid(), true),
				},
				references);
		}
	}
}