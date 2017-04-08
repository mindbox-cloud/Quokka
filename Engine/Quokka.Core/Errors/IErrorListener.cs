using System.Collections.Generic;

namespace Mindbox.Quokka
{
	internal interface IErrorListener
	{
		IReadOnlyCollection<ITemplateError> GetErrors();
	}
}