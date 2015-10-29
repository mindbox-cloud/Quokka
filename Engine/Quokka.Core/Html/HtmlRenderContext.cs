using System;

namespace Quokka.Html
{
	internal class HtmlRenderContext : RenderContext
	{
		public Func<Guid, string, string> RedirectLinkProcessor { get; } 

		public HtmlRenderContext(
			RuntimeVariableScope variableScope,
			FunctionRegistry functions, Func<Guid,
			string, string> redirectLinkProcessor)
			: base(variableScope, functions)
		{
			RedirectLinkProcessor = redirectLinkProcessor;
		}

		public override RenderContext CreateInnerContext(RuntimeVariableScope variableScope)
		{
			return new HtmlRenderContext(variableScope, Functions, RedirectLinkProcessor);
		}
	}
}
