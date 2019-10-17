using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mindbox.Quokka.Html
{
	internal class LinkBlock : TemplateNodeBase, IStaticBlockPart
	{
		public int Offset => hrefValue.Offset;
		public int Length => hrefValue.Length;

		private readonly AttributeValue hrefValue;
		private readonly AttributeValue nameValue;

		private readonly Guid uniqueKey;

		public LinkBlock(AttributeValue hrefValue, AttributeValue nameValue)
		{
			this.hrefValue = hrefValue;
			this.nameValue = nameValue;
			uniqueKey = Guid.NewGuid();
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			if (!hrefValue.IsQuoted)
			{
				var htmlErrorSubListener = context.ErrorListener.GetRegisteredSubListener<HtmlSemanticErrorSubListener>();
				htmlErrorSubListener.AddHrefAttributeMustBeQuotedError(hrefValue.Location);
			}

			foreach (var component in hrefValue.TextComponents)
			{
				component.PerformSemanticAnalysis(context);
			}
		}

		public override void Render(TextWriter resultWriter, RenderContext renderContext)
		{
			var htmlRenderContext = (HtmlRenderContext)renderContext;

			var linkBuilder = new StringBuilder();
			using (var linkWriter = new StringWriter(linkBuilder))
			{
				foreach (var component in hrefValue.TextComponents)
				{
					component.Render(linkWriter, renderContext);
				}
			}

			string redirectUrl = linkBuilder.ToString();
			if (htmlRenderContext.RedirectLinkProcessor != null)
			{
				string processedRedirectUrl = htmlRenderContext.RedirectLinkProcessor(uniqueKey, redirectUrl);
				resultWriter.Write(processedRedirectUrl);
			}
			else
			{
				resultWriter.Write(redirectUrl);
			}
		}

		public override void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context)
		{
			var htmlContext = (HtmlDataAnalysisContext)context;

			htmlContext.AddReference(
				new Reference(
					hrefValue.Text,
					nameValue?.Text,
					uniqueKey,
					isConstant: !hrefValue.TextComponents.OfType<OutputInstructionBlock>().Any()));
		}
	}
}
