using System.Text;

namespace Quokka
{
	internal interface ITemplateNode
	{
		void CompileVariableDefinitions(CompilationVariableScope scope);

		void Render(StringBuilder resultBuilder, RuntimeVariableScope variableScope);
	}
}
