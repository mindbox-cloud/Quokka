using System.Collections.Generic;

namespace Mindbox.Quokka.Html
{
	internal class HtmlDataAnalysisContext : GrammarSpecificDataAnalysisContext
	{
		private readonly List<Reference> references = new List<Reference>();

		public IReadOnlyList<Reference> GetReferences()
		{
			return references.AsReadOnly();
		}

		public void AddReference(Reference reference)
		{
			references.Add(reference);
		}
	}
}
