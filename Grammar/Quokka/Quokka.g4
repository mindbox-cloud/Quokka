grammar Quokka;
options { tokenVocab = QuokkaLex; }

// basic template structure

template
	:
		templateBlock*
		EOF
	;

templateBlock
	:
		(staticBlock | dynamicBlock)+
	;
	
staticBlock
	:
		(constantBlock | outputBlock)+
	;

dynamicBlock
	:
		ifStatement | forStatement  | commentBlock
	;

constantBlock
	:
		Fluff+
	;
		
commentBlock
	:
		SingleInstructionComment
	;

// Conditional statement

ifStatement
	:
		ifCondition
		elseIfCondition*
		elseCondition?
		endIfInstruction
	;
	
ifCondition
	:
		ifInstruction
		templateBlock?
	;

elseCondition
	:
		elseInstruction
		templateBlock?
	;
	
elseIfCondition
	:
		elseIfInstruction
		templateBlock?
	;
	
ifInstruction
	:
		ControlInstructionStart
		If
		booleanExpression
		InstructionEnd
	;

elseIfInstruction
	:
		ControlInstructionStart
		ElseIf
		booleanExpression
		InstructionEnd
	;

elseInstruction
	:
		ControlInstructionStart
		Else
		InstructionEnd
	;

endIfInstruction
	:
		ControlInstructionStart
		EndIf
		InstructionEnd
	;

// Loop statement	

forStatement
	:
		forInstruction
		templateBlock?
		endForInstruction
	;

forInstruction
	:
		ControlInstructionStart
		For	
		iterationVariable 
		In 
		variantValueExpression
		InstructionEnd
	;
	
iterationVariable
	:
		Identifier
	;
	
endForInstruction
	:
		ControlInstructionStart
		EndFor
		InstructionEnd
	;

// Output instructions

outputBlock
	:
		OutputInstructionStart
		expression
		filterChain?
		InstructionEnd
	;
		
filterChain
	:
		(
			Pipe
			functionCallExpression
		)+
	;	

// Expressions

expression
	:
		stringExpression
		| booleanExpression 
		| arithmeticExpression
		| variantValueExpression
	;	

variantValueExpression
	:
		rootVariantValueExpression 
		| memberValueExpression		
	;
	
rootVariantValueExpression
	:
		variableValueExpression	
		| functionCallExpression
	;

variableValueExpression
	:
		Identifier
	;
	
memberValueExpression
	:
		rootVariantValueExpression
		memberAccess
	;
	
memberAccess
	:
		MemberAccessOperator
		member
		memberAccess?
	;
	
member
	:
		property | methodCall
	;
	
property
	:
		Identifier
	;

// Functions and methods

methodCall
	:
		Identifier
		argumentList
	;

functionCallExpression
	:
		(Identifier | If)
		argumentList
	;

argumentList
	:	
		LeftParen
		(
			expression
			(CommaSeparator expression)*
		)?		
		RightParen
	;

// String expressions

stringExpression
	:
		stringConstant
	;	
	
stringConstant
	:
		DoubleQuotedString | SingleQuotedString
	;

// Boolean expressions

booleanExpression
	:
		andExpression
		(Or andExpression)*
	;
	
andExpression
	:
		booleanAtom
		(And booleanAtom)*
	;	

notExpression
	:
		Not booleanAtom
	;
	
parenthesizedBooleanExpression
	:
		LeftParen booleanExpression RightParen	
	;
	
booleanAtom
	:
		variantValueExpression
		| arithmeticComparisonExpression
		| nullComparisonExpression
		| stringComparisonExpression	
		| notExpression
		| parenthesizedBooleanExpression
	;

stringComparisonExpression
	:
		variantValueExpression
		(Equals | NotEquals)
		stringConstant
	;
	
nullComparisonExpression
	:
		variantValueExpression
		(Equals | NotEquals)
		Null
	;

arithmeticComparisonExpression
	:
		arithmeticExpression
		(Equals | NotEquals | LessThan | GreaterThan | LessThanOrEquals | GreaterThanOrEquals)
		arithmeticExpression
	;

// Arithmetic expressions

arithmeticExpression
	:
		multiplicationExpression
		(plusOperand | minusOperand)*
	;
	
plusOperand
	:
		Plus multiplicationExpression
	;
	
minusOperand
	:
		Minus multiplicationExpression
	;

multiplicationExpression
	:
		arithmeticAtom
		(multiplicationOperand | divisionOperand)*
	;
	
multiplicationOperand
	:
		Multiply arithmeticAtom
	;
	
divisionOperand
	:
		Divide arithmeticAtom
	;

negationExpression
	:
		Minus
		arithmeticAtom
	;
	
parenthesizedArithmeticExpression
	:
		LeftParen arithmeticExpression RightParen	
	;
	
arithmeticAtom
	:
		Number
		| variantValueExpression
		| negationExpression
		| parenthesizedArithmeticExpression
	;