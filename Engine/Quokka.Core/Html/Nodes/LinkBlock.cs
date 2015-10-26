using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quokka.Html
{
	internal class LinkBlock : IStaticSubBlock
	{
		/// <summary>
		/// Link url may consist of multiple constant blocks and parameter/expression output blocks,
		/// e.g. "http://${ domain }/index.${ extension }". So we store 
		/// </summary>
		private readonly IReadOnlyList<ITemplateNode> UrlComponents;

		public int Offset { get; }
		public int Length { get; }

		public LinkBlock(IReadOnlyList<ITemplateNode> urlComponents, int offset, int length)
		{
			Offset = offset;
			Length = length;
			UrlComponents = urlComponents;
		}

		public void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			throw new NotImplementedException();
		}

		public void Render(StringBuilder resultBuilder, RenderContext context)
		{
			throw new NotImplementedException();
		}
	}
}
