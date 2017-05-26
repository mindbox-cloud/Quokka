//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Grammar\Quokka\QuokkaHtml.g4 by ANTLR 4.7

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Mindbox.Quokka.Generated {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="QuokkaHtml"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7")]
[System.CLSCompliant(false)]
internal interface IQuokkaHtmlVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaHtml.htmlBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHtmlBlock([NotNull] QuokkaHtml.HtmlBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaHtml.nonImportantHtml"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNonImportantHtml([NotNull] QuokkaHtml.NonImportantHtmlContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaHtml.plainText"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPlainText([NotNull] QuokkaHtml.PlainTextContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaHtml.attribute"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAttribute([NotNull] QuokkaHtml.AttributeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaHtml.attributeValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAttributeValue([NotNull] QuokkaHtml.AttributeValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaHtml.unquotedValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnquotedValue([NotNull] QuokkaHtml.UnquotedValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaHtml.singleQuotedValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSingleQuotedValue([NotNull] QuokkaHtml.SingleQuotedValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaHtml.doubleQuotedValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDoubleQuotedValue([NotNull] QuokkaHtml.DoubleQuotedValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaHtml.insideAttributeOutputBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInsideAttributeOutputBlock([NotNull] QuokkaHtml.InsideAttributeOutputBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaHtml.insideAttributeConstant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInsideAttributeConstant([NotNull] QuokkaHtml.InsideAttributeConstantContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaHtml.openingTag"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOpeningTag([NotNull] QuokkaHtml.OpeningTagContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaHtml.closingTag"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClosingTag([NotNull] QuokkaHtml.ClosingTagContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaHtml.selfClosingTag"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSelfClosingTag([NotNull] QuokkaHtml.SelfClosingTagContext context);
}
} // namespace Mindbox.Quokka.Generated