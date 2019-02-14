grammar Quokka;
options { tokenVocab = QuokkaLex; }

// basic template structure

template
	:
		templateBlock?
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
		ifStatement | forStatement  | assignmentBlock | commentBlock
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

// Assignment

assignmentBlock
	:
		ControlInstructionStart
		Set
		Identifier
		Equals
		expression
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
		variantValueExpression
		| stringExpression
		| booleanExpression 
		| arithmeticExpression
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
		variableValueExpression
		(
			MemberAccessOperator
			member
		)+
	;
	
member
	:
		field | methodCall
	;
	
field
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
		stringLiteral | stringConcatenation
	;	
	
stringConstant
	:
		DoubleQuotedString | SingleQuotedString
	;
	
stringLiteral
	:
		StringInterpolationStart
		
		(
			StringFluff*
			StringParameterStart
			expression
			InstructionEnd
						
		)*
		DoubleQuotedStringEnd
	;

stringConcatenation
	:
		stringAtom Ampersand expression
	;
	
stringAtom
	:
		variantValueExpression | stringConstant
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

stringComparisonExpression
	:
		variantValueExpression
		(Equals | NotEquals)
		stringExpression
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
	
booleanAtom
	:
		variantValueExpression
		| notExpression
		| parenthesizedBooleanExpression
		| stringComparisonExpression
		| nullComparisonExpression	
		| arithmeticComparisonExpression		
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