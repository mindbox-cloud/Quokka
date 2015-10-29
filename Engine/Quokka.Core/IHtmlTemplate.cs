using System;
using System.Collections.Generic;

using Quokka.Html;

namespace Quokka
{
	public interface IHtmlTemplate : ITemplate
	{
		/// <summary>
		/// Get a collection of external references (links) ordered from top to bottom.
		/// </summary>
		IReadOnlyList<Reference> GetReferences();
		
		/// <summary>
		/// Render Html message
		/// </summary>
		/// <param name="model">Model for parameters</param>
		/// <param name="redirectLinkProcessor">Action that will be applied to each link url</param>
		/// <returns>Rendered Html message</returns>
		string Render(ICompositeModelValue model, Func<Guid, string, string> redirectLinkProcessor);
	}
}