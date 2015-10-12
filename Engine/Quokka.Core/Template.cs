using System;
using System.Linq;
using System.Text;
using Antlr4.Runtime;
using Quokka.Generated;

namespace Quokka
{
	public sealed class Template
	{
		private readonly TemplateBlock rootBlock;
		private readonly ICompositeModelDefinition externalModelDefinition;
		private readonly FunctionRegistry functionRegistry;

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

			functionRegistry = new FunctionRegistry(new TemplateFunction[]
			{
				new ToUpperTemplateFunction(),
				new ToLowerTemplateFunction(),
				new ReplaceIfEmptyTemplateFunction(),
				new FormatDecimalTemplateFunction(),
				new FormatDateTimeTemplateFunction(),
				new FormatTimeTemplateFunction(),
				new IfTemplateFunction()
			});

			var scope = new CompilationVariableScope();

			var errorListener = new SemanticErrorListener();
			rootBlock.CompileVariableDefinitions(new SemanticAnalysisContext(scope, functionRegistry, errorListener));
			externalModelDefinition = scope.Variables.ToModelDefinition(errorListener);

			var errors = errorListener.GetErrors();
			if (errors.Any())
				throw new InvalidOperationException(
					string.Join(
						Environment.NewLine,
						errors.Select(error => error.Message)));
		}

		public ICompositeModelDefinition GetModelDefinition()
		{
			return externalModelDefinition;
		}

		public string Apply(ICompositeModelValue model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			var valueStorage = VariableValueStorage.CreateStorageForValue(model);
			var builder = new StringBuilder();
			var context = new RenderContext(new RuntimeVariableScope(valueStorage), functionRegistry);
            rootBlock.Render(builder, context);
			return builder.ToString();
		}
	}
}

