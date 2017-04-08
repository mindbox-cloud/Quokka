using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mindbox.Quokka.Html;

namespace Mindbox.Quokka.Generated
{
	internal partial class QuokkaHtmlBaseVisitor<Result>
	{
		protected readonly HtmlBlockParsingContext parsingContext;

		protected QuokkaHtmlBaseVisitor(HtmlBlockParsingContext parsingContext)
		{
			this.parsingContext = parsingContext;
		}
	}
}
