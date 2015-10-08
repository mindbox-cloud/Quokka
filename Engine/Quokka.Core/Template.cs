using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Antlr4.Runtime;

using Quokka.Generated;

namespace Quokka
{
	public class Template
	{
		private readonly TemplateBlock rootBlock;
		private readonly IReadOnlyCollection<IParameterDefinition> parameterDefinitions;

		public Template(string templateText)
		{
			if (templateText == null)
				throw new ArgumentNullException(nameof(templateText));

			var inputStream = new AntlrInputStream(templateText);
			var lexer = new QuokkaLex(inputStream);
			var commonTokenStream = new CommonTokenStream(lexer);
			var parser = new QuokkaParser(commonTokenStream);

			var compilationVisitor = new RootTemplateVisitor();
			rootBlock = compilationVisitor.Visit(parser.template()) ?? TemplateBlock.Empty();

			if (parser.NumberOfSyntaxErrors > 0)
				throw new InvalidOperationException("Syntax errors in the template");

			var scope = new CompilationVariableScope();
			rootBlock.CompileVariableDefinitions(scope);

			var errorListener = new SemanticErrorListener();
			parameterDefinitions = scope.Variables.GetParameterDefinitions(errorListener);

			var errors = errorListener.GetErrors();
			if (errors.Any())
				throw new InvalidOperationException(
					string.Concat(
						errors.Select(error => error.Message)));
		}

		public IList<IParameterDefinition> GetParameterDefinitions()
		{
			return parameterDefinitions.ToList();
		}

		public string Apply(ICompositeParameterValue model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			var valueStorage = VariableValueStorage.CreateStorageForValue(model);
			var builder = new StringBuilder();
			rootBlock.Render(builder, new RuntimeVariableScope(valueStorage));
			return builder.ToString();
		}
	}
}

