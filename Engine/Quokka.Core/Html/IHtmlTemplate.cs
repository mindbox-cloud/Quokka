using System;
using System.Collections.Generic;

namespace Mindbox.Quokka.Html
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
		/// <param name="identificationCode">Html code that will be rendered at the end of the document (if specified)</param>
		/// <returns>Rendered Html message</returns>
		string Render(
			ICompositeModelValue model, 
			Func<Guid, string, string> redirectLinkProcessor,
			string identificationCode);
	}
}