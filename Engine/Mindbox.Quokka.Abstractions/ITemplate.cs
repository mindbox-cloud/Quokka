using System.Collections.Generic;
using System.IO;

namespace Mindbox.Quokka
{
	public interface ITemplate
	{
		bool IsConstant { get; }

		IList<ITemplateError> Errors { get; }

		ICompositeModelDefinition GetModelDefinition();

		string Render(ICompositeModelValue model, ICallContextContainer callContextContainer = null);

		void Render(TextWriter textWriter, ICompositeModelValue model, ICallContextContainer callContextContainer = null);
	}
}
