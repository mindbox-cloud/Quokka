// // Copyright 2022 Mindbox Ltd
// //
// // Licensed under the Apache License, Version 2.0 (the "License");
// // you may not use this file except in compliance with the License.
// // You may obtain a copy of the License at
// //
// //     http://www.apache.org/licenses/LICENSE-2.0
// //
// // Unless required by applicable law or agreed to in writing, software
// // distributed under the License is distributed on an "AS IS" BASIS,
// // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// // See the License for the specific language governing permissions and
// // limitations under the License.

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



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
/// by <see cref="QuokkaParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.2")]
[System.CLSCompliant(false)]
internal interface IQuokkaVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.template"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTemplate([NotNull] QuokkaParser.TemplateContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.templateBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTemplateBlock([NotNull] QuokkaParser.TemplateBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.staticBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStaticBlock([NotNull] QuokkaParser.StaticBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.dynamicBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDynamicBlock([NotNull] QuokkaParser.DynamicBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.constantBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstantBlock([NotNull] QuokkaParser.ConstantBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.commentBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCommentBlock([NotNull] QuokkaParser.CommentBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.ifStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfStatement([NotNull] QuokkaParser.IfStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.ifCondition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfCondition([NotNull] QuokkaParser.IfConditionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.elseCondition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseCondition([NotNull] QuokkaParser.ElseConditionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.elseIfCondition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseIfCondition([NotNull] QuokkaParser.ElseIfConditionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.ifInstruction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfInstruction([NotNull] QuokkaParser.IfInstructionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.elseIfInstruction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseIfInstruction([NotNull] QuokkaParser.ElseIfInstructionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.elseInstruction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseInstruction([NotNull] QuokkaParser.ElseInstructionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.endIfInstruction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEndIfInstruction([NotNull] QuokkaParser.EndIfInstructionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.forStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForStatement([NotNull] QuokkaParser.ForStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.forInstruction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForInstruction([NotNull] QuokkaParser.ForInstructionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.iterationVariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIterationVariable([NotNull] QuokkaParser.IterationVariableContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.endForInstruction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEndForInstruction([NotNull] QuokkaParser.EndForInstructionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.assignmentBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignmentBlock([NotNull] QuokkaParser.AssignmentBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.outputBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOutputBlock([NotNull] QuokkaParser.OutputBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.filterChain"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFilterChain([NotNull] QuokkaParser.FilterChainContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression([NotNull] QuokkaParser.ExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.variantValueExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariantValueExpression([NotNull] QuokkaParser.VariantValueExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.rootVariantValueExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRootVariantValueExpression([NotNull] QuokkaParser.RootVariantValueExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.variableValueExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableValueExpression([NotNull] QuokkaParser.VariableValueExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.memberValueExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMemberValueExpression([NotNull] QuokkaParser.MemberValueExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.member"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMember([NotNull] QuokkaParser.MemberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.field"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitField([NotNull] QuokkaParser.FieldContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.methodCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMethodCall([NotNull] QuokkaParser.MethodCallContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.functionCallExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCallExpression([NotNull] QuokkaParser.FunctionCallExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.argumentList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArgumentList([NotNull] QuokkaParser.ArgumentListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.stringExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStringExpression([NotNull] QuokkaParser.StringExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.stringConstant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStringConstant([NotNull] QuokkaParser.StringConstantContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.stringConcatenation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStringConcatenation([NotNull] QuokkaParser.StringConcatenationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.stringAtom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStringAtom([NotNull] QuokkaParser.StringAtomContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.booleanExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBooleanExpression([NotNull] QuokkaParser.BooleanExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.andExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAndExpression([NotNull] QuokkaParser.AndExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.notExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNotExpression([NotNull] QuokkaParser.NotExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.parenthesizedBooleanExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenthesizedBooleanExpression([NotNull] QuokkaParser.ParenthesizedBooleanExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.stringComparisonExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStringComparisonExpression([NotNull] QuokkaParser.StringComparisonExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.nullComparisonExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNullComparisonExpression([NotNull] QuokkaParser.NullComparisonExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.arithmeticComparisonExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArithmeticComparisonExpression([NotNull] QuokkaParser.ArithmeticComparisonExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.booleanAtom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBooleanAtom([NotNull] QuokkaParser.BooleanAtomContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.arithmeticExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArithmeticExpression([NotNull] QuokkaParser.ArithmeticExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.plusOperand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPlusOperand([NotNull] QuokkaParser.PlusOperandContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.minusOperand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMinusOperand([NotNull] QuokkaParser.MinusOperandContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.multiplicationExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultiplicationExpression([NotNull] QuokkaParser.MultiplicationExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.multiplicationOperand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultiplicationOperand([NotNull] QuokkaParser.MultiplicationOperandContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.divisionOperand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDivisionOperand([NotNull] QuokkaParser.DivisionOperandContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.negationExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNegationExpression([NotNull] QuokkaParser.NegationExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.parenthesizedArithmeticExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenthesizedArithmeticExpression([NotNull] QuokkaParser.ParenthesizedArithmeticExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="QuokkaParser.arithmeticAtom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArithmeticAtom([NotNull] QuokkaParser.ArithmeticAtomContext context);
}
} // namespace Mindbox.Quokka.Generated
