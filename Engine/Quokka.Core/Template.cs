using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr4.Runtime;

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class Template : ITemplate
	{
		private readonly TemplateBlock compiledTemplateTree;
		private readonly ICompositeModelDefinition requiredModelDefinition;
		private readonly FunctionRegistry functionRegistry;
		
		public IList<ITemplateError> Errors { get; }

		/// <summary>
		/// <c>True</c> if the template is a constant string that is independent from parameters
		/// and will always be rendered the same way, otherwise <c>False</c>.
		/// </summary>
		public bool IsConstant { get; }

		internal Template(
			string templateText,
			FunctionRegistry functionRegistry,
			bool throwIfErrorsEncountered = true,
			Func<VisitingContext, IQuokkaVisitor<StaticBlock>> staticBlockVisitorCreator = null,
			IEnumerable<SemanticErrorSubListenerBase> semanticErrorSubListeners = null)
		{
			if (templateText == null)
				throw new ArgumentNullException(nameof(templateText));
			if (functionRegistry == null)
				throw new ArgumentNullException(nameof(functionRegistry));
			
			this.functionRegistry = functionRegistry;

			try
			{
				var syntaxErrorListener = new SyntaxErrorListener();
				var semanticErrorListener = new SemanticErrorListener();
				if (semanticErrorSubListeners != null)
				{
					foreach (var subListener in semanticErrorSubListeners)
					{
						semanticErrorListener.RegisterSubListener(subListener);
					}
				}

				var templateParseTree = ParseTemplateText(templateText, syntaxErrorListener);

				if (!syntaxErrorListener.GetErrors().Any())
				{
					VisitingContext visitingContext = new VisitingContext(
						syntaxErrorListener,
						staticBlockVisitorCreator ?? (context => new StaticBlockVisitor(context)));

					compiledTemplateTree = new RootTemplateVisitor(visitingContext).Visit(templateParseTree);

					IsConstant = compiledTemplateTree.IsConstant;

					var analysisContext = new AnalysisContext
						(new CompilationVariableScope(),
						functionRegistry,
						semanticErrorListener);

					compiledTemplateTree.PerformSemanticAnalysis(analysisContext);
					analysisContext.VariableScope.Compile(analysisContext);
					requiredModelDefinition = ValueUsageSummary.ConvertCollectionToModelDefinition(
						analysisContext.VariableScope.Variables,
						semanticErrorListener);
				}

				Errors =
					syntaxErrorListener.GetErrors()
						.Concat(semanticErrorListener.GetErrors())
						.ToList();

				if (throwIfErrorsEncountered && Errors.Any())
					throw new TemplateContainsErrorsException(Errors);
			}
			catch (Exception ex)
			{
				if (ex is TemplateException)
					throw;

				throw new TemplateException("Unexpected errors occured during template creation", ex);
			}
		}
		
		public Template(string templateText)
			: this(templateText, new FunctionRegistry(GetStandardFunctions()), true)
		{
		}

		public ICompositeModelDefinition GetModelDefinition()
		{
			return requiredModelDefinition;
		}

		public virtual string Render(ICompositeModelValue model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));
			if (Errors.Any())
				throw new TemplateContainsErrorsException(Errors);

			return DoRender(model, (scope, contextFunctionRegistry) => new RenderContext(scope, contextFunctionRegistry));
		}

		protected string DoRender(
			ICompositeModelValue model,
			Func<RuntimeVariableScope, FunctionRegistry, RenderContext> renderContextCreator)
		{
			try
			{
				new ModelValidator().ValidateModel(model, requiredModelDefinition);

				var valueStorage = new CompositeVariableValueStorage(model);
				var builder = new StringBuilder();
				var context = renderContextCreator(new RuntimeVariableScope(valueStorage), functionRegistry);
				compiledTemplateTree.Render(builder, context);
				context.OnRenderingEnd(builder);
				return builder.ToString();
			}
			catch (Exception ex)
			{
				if (ex is TemplateException)
					throw;

				throw new TemplateException("Unexpected errors occured during template rendering", ex);
			}
		}

		protected void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context)
		{
			compiledTemplateTree.CompileGrammarSpecificData(context);
		}

		private QuokkaParser.TemplateContext ParseTemplateText(string templateText, SyntaxErrorListener syntaxErrorListener)
		{
			var inputStream = new CodePointCharStream(templateText);
			var commonTokenStream = new CommonTokenStream(new QuokkaLex(inputStream));

			var parser = new QuokkaParser(commonTokenStream);
			parser.RemoveErrorListeners();
			parser.AddErrorListener(syntaxErrorListener);

			return parser.template();
		}

		internal static IEnumerable<TemplateFunction> GetStandardFunctions()
		{
			yield return new ToUpperTemplateFunction();
			yield return new ToLowerTemplateFunction();
			yield return new ReplaceIfEmptyTemplateFunction();
			yield return new FormatDecimalTemplateFunction();
			yield return new FormatDateTimeTemplateFunction();
			yield return new FormatTimeTemplateFunction();
			yield return new IfTemplateFunction();
			yield return new IsEmptyTemplateFunction();
			yield return new TableRowsTemplateFunction();
			yield return new CountTemplateFunction();
			yield return new AddDaysTemplateFunction();
			yield return new ChooseRandomTextFunction();
			yield return new FloorTemplateFunction();
			yield return new CeilingTemplateFunction();
			yield return new SubstringTemplateFunction();
			yield return new SubstringWithLengthTemplateFunction();
			yield return new LengthTemplateFunction();
			yield return new GetDayTemplateFunction();
			yield return new GetMonthTemplateFunction();
			yield return new GetYearTemplateFunction();
		}
	}
}

