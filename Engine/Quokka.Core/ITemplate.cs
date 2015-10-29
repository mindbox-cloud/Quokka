using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quokka
{
	public interface ITemplate
	{
		IList<ITemplateError> Errors { get; }

		ICompositeModelDefinition GetModelDefinition();

		string Render(ICompositeModelValue model);
	}
}
